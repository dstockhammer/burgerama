using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;

namespace Burgerama.Services.Outings.Api.Controllers
{
    public sealed class OutingController : ApiController
    {
        private readonly IOutingRepository _outingRepository;

        public OutingController(IOutingRepository outingRepository)
        {
            _outingRepository = outingRepository;
        }

        /// <summary>
        /// Get all outings.
        /// </summary>
        /// <example>
        /// GET /outing
        /// </example>
        /// <returns>Returns all outings.</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<Outing>))]
        public IHttpActionResult GetAllOutings()
        {
            var outings = _outingRepository.GetAll();
            return Ok(outings);
        }

        /// <summary>
        /// Get an outing by id.
        /// </summary>
        /// <example>
        /// GET /outing/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example>
        /// <param name="outingId">The guid of the outing.</param>
        /// <returns>Returns the outing with the passed id.</returns>
        [HttpGet]
        [Route("{outingId}")]
        [ResponseType(typeof(Outing))]
        public IHttpActionResult GetOutingById(Guid outingId)
        {
            var venue = _outingRepository.Get(outingId);

            if (venue == null)
                return NotFound();

            return Ok(venue);
        }
    }
}