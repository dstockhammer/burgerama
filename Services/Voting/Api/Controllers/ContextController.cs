using Burgerama.Services.Voting.Api.Converters;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using System.Web.Http;
using System.Web.Http.Description;

namespace Burgerama.Services.Voting.Api.Controllers
{
    public class ContextController : ApiController
    {
        private readonly IContextRepository _contextRepository;

        public ContextController(IContextRepository contextRepository)
        {
            _contextRepository = contextRepository;
        }

        [HttpGet]
        [Route("context/{contextKey}")]
        [ResponseType(typeof(ContextModel))]
        public IHttpActionResult GetContext(string contextKey)
        {
            var context = _contextRepository.Get(contextKey);
            if (context == null) return NotFound();

            return Ok(context.Candidates);
        }

        [HttpPost]
        [Route("context/{contextKey}")]
        [ResponseType(typeof(ContextModel))]
        public IHttpActionResult CreateContext(string contextKey)
        {
            var context = _contextRepository.Get(contextKey);
            if (context != null) return Conflict();

            context = new Context(contextKey);
            _contextRepository.SaveOrUpdate(new Context(contextKey));
            return Created(string.Format("context/{0}", contextKey), context.ToModel());
        }
    }
}
