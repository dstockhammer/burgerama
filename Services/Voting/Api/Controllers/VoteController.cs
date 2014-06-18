using System.Security.Claims;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Voting.Api.Converters;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Burgerama.Services.Voting.Api.Controllers
{
    public class VoteController : ApiController
    {
        private readonly IContextRepository _contextRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public VoteController(ICandidateRepository candidateRepository, IContextRepository contextRepository, IEventDispatcher eventDispatcher)
        {
            _candidateRepository = candidateRepository;
            _contextRepository = contextRepository;
            _eventDispatcher = eventDispatcher;
        }

        [HttpGet]
        [Route("{contextKey}/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult GetVotes(string contextKey, Guid reference)
        {
            var candidate = _candidateRepository.Get(contextKey, reference);

            if (candidate == null)
                return NotFound();

            return Ok(candidate.Votes.Select(v => v.ToModel()));
        }

        [Authorize]
        [HttpPost]
        [Route("{contextKey}/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult Vote(string contextKey, Guid reference)
        {
            var userId = ClaimsPrincipal.Current.GetUserId();
            var context = _contextRepository.Get(contextKey);

            if (context == null)
                return NotFound();
            
            var vote = new Vote(DateTime.Now, userId);

            var candidate = _candidateRepository.Get(contextKey, reference);

            // If this is the first vote
            if (candidate == null)
            {
                _eventDispatcher.Publish(new TriedToVoteUnknownCandidate
                {
                    Reference = reference,
                    ContextKey = contextKey,
                    UserId = userId,
                    VotedOn = vote.CreatedOn
                });

                return Ok();
            }

            candidate.AddVote(vote);
            _candidateRepository.SaveOrUpdate(candidate);

            _eventDispatcher.Publish(new VoteAdded
            {
                CandidateReference = candidate.Reference,
                ContextKey = contextKey,
                UserId = userId,
                TotalOfVotes = candidate.Votes.Count()
            });

            var uri = new Uri(Url.Content(string.Format("~/{0}/{1}/votes", contextKey, reference)));
            return Created(uri, candidate.Votes.Select(v => v.ToModel()));
        }
    }
}
