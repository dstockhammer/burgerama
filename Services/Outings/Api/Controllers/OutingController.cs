using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Services.Outings.Api.Converters;
using Burgerama.Services.Outings.Api.Models;
using Burgerama.Services.Outings.Domain.Contracts;

namespace Burgerama.Services.Outings.Api.Controllers
{
    public sealed class OutingController : ApiController
    {
        private readonly IOutingRepository _outingRepository;
        private readonly IVenueRepository _venueRepository;

        public OutingController(IOutingRepository outingRepository, IVenueRepository venueRepository)
        {
            _outingRepository = outingRepository;
            _venueRepository = venueRepository;
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
        [ResponseType(typeof(IEnumerable<OutingModel>))]
        public IHttpActionResult GetAllOutings()
        {
            var outings = _outingRepository.GetAll()
                .Select(o => o.ToModel(venueId => _venueRepository.Get(venueId)));

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
        [ResponseType(typeof(OutingModel))]
        public IHttpActionResult GetOutingById(Guid outingId)
        {
            var outing = _outingRepository.Get(outingId);
            if (outing == null)
                return NotFound();

            return Ok(outing.ToModel());
        }
    }
}