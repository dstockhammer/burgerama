using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Services.Voting.Api.Converters;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Route("context/{contextKey}/candidate/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult GetVotes(string contextKey, Guid reference)
        {
            var candidate = _candidateRepository.Get(reference, contextKey);

            if (candidate == null)
                return NotFound();

            return Ok(candidate.Votes.Select(v => v.ToModel()));
        }

        [HttpPost]
        [Route("context/{contextKey}/candidate/{reference}/votes")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult CreateContext(string contextKey, Guid reference, [FromBody]DateTime? votedOn = null)
        {
            var context = _contextRepository.Get(contextKey);
            if (context == null) return NotFound();

            var candidate = _candidateRepository.Get(reference, contextKey);

            if (candidate == null)
            {
                //candidate = new Candidate(reference);
                //candidate.Vote(ClaimsPrincipal.Current.GetUserId(), votedOn ?? DateTime.Now);
                // ENQUEUE CANDIDATE
                // RAISE EVENT: CANDIDATEENQUEUED
                // ON COMMAND: CREATECANDIDATE
                // DEQUEUE CANDIDATE
                //var context = _contextRepository.Get(contextKey);
                //context.AddCandidate(reference);
                //_contextRepository.SaveOrUpdate(context);
                //_candidateRepository.SaveOrUpdate(candidate, key);
                // RAISE EVENT VOTEADDED
            }
            else
            {
                candidate.Vote(ClaimsPrincipal.Current.GetUserId(), votedOn ?? DateTime.Now);
                _candidateRepository.SaveOrUpdate(candidate, contextKey);
                // RAISE EVENT: VOTEADDED
            }

            return Created(string.Format("context/{0}/candidate/{1}/votes", contextKey, reference), candidate.Votes.Select(v => v.ToModel()));
        }
    }
}
