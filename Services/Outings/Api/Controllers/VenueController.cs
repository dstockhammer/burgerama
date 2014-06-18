using Burgerama.Services.Outings.Api.Converters;
using Burgerama.Services.Outings.Api.Models;
using Burgerama.Services.Outings.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Burgerama.Services.Outings.Api.Controllers
{
    public sealed class VenueController : ApiController
    {
        private readonly IOutingRepository _outingRepository;
        private readonly IVenueRepository _venueRepository;

        public VenueController(IOutingRepository outingRepository, IVenueRepository venueRepository)
        {
            _outingRepository = outingRepository;
            _venueRepository = venueRepository;
        }

        [HttpGet]
        [Route("venues/{venueId}")]
        [ResponseType(typeof(IEnumerable<OutingModel>))]
        public IHttpActionResult GetOutingsByVenueId(Guid venueId)
        {
            var outings = _outingRepository.GetAllForVenue(venueId)
                .Select(o => o.ToModel(vId => _venueRepository.Get(vId)));

            return Ok(outings);
        }

    }
}