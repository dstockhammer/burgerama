using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Voting.Api.Converters;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Shared.Candidates.Domain.Contracts;
using Serilog;

namespace Burgerama.Services.Voting.Api.Controllers
{
    public class VoteController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IContextRepository _contextRepository;
        private readonly ICandidateRepository _candidateRepository;

        public VoteController(ILogger logger, IEventDispatcher eventDispatcher, IContextRepository contextRepository, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _contextRepository = contextRepository;
            _candidateRepository = candidateRepository;
        }

        [HttpGet]
        [Route("{contextKey}/{reference}")]
        [ResponseType(typeof(CandidateModel))]
        public IHttpActionResult GetRatingSummaryForCandidate(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            // first, check all real (= well known) candidates
            var candidate = _candidateRepository.Get<Candidate, Vote>(contextKey, reference);
            if (candidate != null)
                return Ok(candidate.ToModel(ClaimsPrincipal.Current.GetUserId()));

            // if there is no real candidate, check the potential ones
            var potentialCandidate = _candidateRepository.GetPotential<PotentialCandidate, Vote>(contextKey, reference);
            if (potentialCandidate != null)
                return Ok(potentialCandidate.ToModel(ClaimsPrincipal.Current.GetUserId()));

            // if no candidate is found at all, check if the context is valid
            var context = _contextRepository.Get(contextKey);
            if (context == null)
                return BadRequest("Invalid context.");

            // if the context is valid, still return an empty, not validated model in order to allow users to vote
            return Ok(new CandidateModel
            {
                ContextKey = contextKey,
                Reference = reference,
                IsValidated = false,
                CanUserVote = true
            });
        }

        [HttpGet]
        [Route("{contextKey}/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult GetAllRatingsForCandidate(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            // first, check all real (= well known) candidates
            var candidate = _candidateRepository.Get<Candidate, Vote>(contextKey, reference);
            if (candidate != null)
                return Ok(candidate.Items.Select(r => r.ToModel()));

            // if there is no real candidate, check the potential ones
            var potentialCandidate = _candidateRepository.GetPotential<PotentialCandidate, Vote>(contextKey, reference);
            if (potentialCandidate != null)
                Ok(potentialCandidate.Items.Select(r => r.ToModel()));

            // if no candidate is found at all, check if the context is valid,
            // in order to determine the correct error type
            var context = _contextRepository.Get(contextKey);
            if (context == null)
                return BadRequest("Invalid context.");

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        [Route("{contextKey}/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult AddRatingToCandidate(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            
            var userId = ClaimsPrincipal.Current.GetUserId();

            // todo: validate vote?
            var vote = new Vote(DateTime.Now, userId);

            var candidate = _candidateRepository.Get<Candidate, Vote>(contextKey, reference);
            if (candidate != null)
            {
                if (candidate.Items.Any(r => r.UserId == userId))
                    return Conflict();

                var events = candidate.AddItem(vote);
                _candidateRepository.SaveOrUpdate(candidate);
                _eventDispatcher.Publish(events);

                _logger.Information("Added vote to candidate {Reference}. New total is {NewTotal}.",
                    reference, candidate.Items.Count());

                var candidateUri = new Uri(Url.Content(string.Format("~/{0}/{1}", contextKey, reference)));
                return Created(candidateUri, candidate.ToModel(userId));
            }

            var potentialCandidate = _candidateRepository.GetPotential<PotentialCandidate, Vote>(contextKey, reference);
            if (potentialCandidate == null)
            {
                // if there isn't already a potential candidate, validate the context
                var context = _contextRepository.Get(contextKey);
                if (context == null)
                    return BadRequest("Invalid context.");

                // if the context is valid, use it to determine whether voting on an unknown candidate is allowed
                if (context.GracefullyHandleUnknownCandidates == false)
                    return NotFound();

                potentialCandidate = new PotentialCandidate(contextKey, reference);
            }

            if (potentialCandidate.Items.Any(r => r.UserId == userId))
                return Conflict();

            potentialCandidate.AddItem(vote);

            _candidateRepository.SaveOrUpdate(potentialCandidate);
            _eventDispatcher.Publish(new TriedToVoteForUnknownCandidate
            {
                ContextKey = contextKey,
                Reference = reference,
                UserId = userId
            });

            _logger.Information("Added vote to unknown candidate {Reference}. New total is {NewTotal}.",
                reference, potentialCandidate.Items.Count());

            var uri = new Uri(Url.Content(string.Format("~/{0}/{1}", contextKey, reference)));
            return Created(uri, potentialCandidate.ToModel(userId));
        }
    }
}
