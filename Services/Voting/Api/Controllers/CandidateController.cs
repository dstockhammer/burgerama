using Burgerama.Messaging.Events;
using Burgerama.Services.Voting.Api.Converters;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain.Contracts;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace Burgerama.Services.Voting.Api.Controllers
{
    public class CandidateController : ApiController
    {
        private readonly IContextRepository _contextRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CandidateController(ICandidateRepository candidateRepository, IContextRepository contextRepository, IEventDispatcher eventDispatcher)
        {
            _candidateRepository = candidateRepository;
            _contextRepository = contextRepository;
            _eventDispatcher = eventDispatcher;
        }

        [HttpGet]
        [Route("context/{contextKey}/candidate/{reference}")]
        [ResponseType(typeof(CandidateModel))]
        public IHttpActionResult GetCandidate(string contextKey, Guid reference)
        {
            var candidate = _candidateRepository.Get(reference, contextKey);

            if (candidate == null)
                return NotFound();

            return Ok(candidate.ToModel());
        }

        [HttpPost]
        [Route("context/{contextKey}/candidate/{reference}")]
        [ResponseType(typeof(CandidateModel))]
        public IHttpActionResult CreateContext(string contextKey, Guid reference)
        {
            var context = _contextRepository.Get(contextKey);
            if (context == null) return NotFound();

            var candidate = _candidateRepository.Get(reference, contextKey);
            if (candidate != null) return Conflict();
                
            //_eventDispatcher.Publish();
            //candidate = new Candidate(reference);

            // ENQUEUE CANDIDATE
            // RAISE EVENT: CANDIDATEENQUEUED
            // ON COMMAND: CREATECANDIDATE
            // DEQUEUE CANDIDATE
            //var context = _contextRepository.Get(contextKey);
            //context.AddCandidate(reference);
            //_contextRepository.SaveOrUpdate(context);
            //_candidateRepository.SaveOrUpdate(candidate, key);

            return Created(string.Format("context/{0}/candidate/{1}", contextKey, reference), candidate.ToModel());
        }
    }
}
