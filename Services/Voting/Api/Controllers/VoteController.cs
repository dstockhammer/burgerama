using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Voting.Api.Converters;
using Burgerama.Services.Voting.Api.Models;
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

        public VoteController(
            ICandidateRepository candidateRepository, 
            IContextRepository contextRepository, 
            IEventDispatcher eventDispatcher)
        {
            _candidateRepository = candidateRepository;
            _contextRepository = contextRepository;
            _eventDispatcher = eventDispatcher;
        }

        [HttpGet]
        [Route("{contextKey}/candidates/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult GetVotes(string contextKey, Guid reference)
        {
            var candidate = _candidateRepository.Get(reference, contextKey);

            if (candidate == null)
                return NotFound();

            return Ok(candidate.Votes.Select(v => v.ToModel()));
        }

        [HttpPost]
        [Route("{contextKey}/candidates/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult Vote(string contextKey, Guid reference, [FromBody]DateTime? votedOn)
        {
            var userId = "test";//ClaimsPrincipal.Current.GetUserId();
            var context = _contextRepository.Get(contextKey);
            var voted = votedOn ?? DateTime.Now;

            if (context == null) return NotFound();

            var candidate = _candidateRepository.Get(reference, contextKey);

            // If this is the first vote
            if (candidate == null)
            {
                _eventDispatcher.Publish(new TriedToVoteUnknownCandidate
                {
                    Reference = reference,
                    ContextKey = contextKey,
                    UserId = userId,
                    VotedOn = voted
                });

                return Ok();
            }

            candidate.Vote(userId, voted);
            _candidateRepository.SaveOrUpdate(candidate, contextKey);

            _eventDispatcher.Publish(new VoteAdded
            {
                CandidateReference = candidate.Reference,
                ContextKey = contextKey,
                UserId = userId,
                TotalOfVotes = candidate.Votes.Count()
            });

            var uri = new Uri(this.Url.Content(string.Format("~/{0}/candidates/{1}/votes", contextKey, reference)));
            return Created(uri, candidate.Votes.Select(v => v.ToModel()));
        }
    }
}
