using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Venues.Api.Converters;
using Burgerama.Services.Venues.Api.Models;
using Burgerama.Services.Venues.Domain;
using Burgerama.Services.Venues.Domain.Contracts;
using Serilog;

namespace Burgerama.Services.Venues.Api.Controllers
{
    public class VenueController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IVenueRepository _venueRepository;

        public VenueController(ILogger logger, IEventDispatcher eventDispatcher, IVenueRepository venueRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _venueRepository = venueRepository;
        }

        /// <summary>
        /// Get all venues.
        /// </summary>
        /// <example>
        /// GET /venue
        /// </example>
        /// <returns>Returns all venues.</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<VenueModel>))]
        public IHttpActionResult GetAllVenues()
        {
            var venues = _venueRepository.GetAll()
                .Select(v => v.ToModel());

            return Ok(venues);
        }

        /// <summary>
        /// Get a venue by id.
        /// </summary>
        /// <example>
        /// GET /venue/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example>
        /// <param name="venueId">The guid of the venue.</param>
        /// <returns>Returns the venue with the passed id.</returns>
        [HttpGet]
        [Route("{venueId}")]
        [ResponseType(typeof(VenueModel))]
        public IHttpActionResult GetVenueById(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            if (venue == null)
                return NotFound();

            return Ok(venue.ToModel());
        }

        /// <summary>
        /// Add a venue.
        /// </summary>
        /// <remarks>
        /// This operation requires authentication.
        /// </remarks>
        /// <example>
        /// POST /venue/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example>
        /// <param name="model">The venue that should be added.</param>
        /// <returns>Returns the location of the newly added venue.</returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddVenue(VenueModel model)
        {
            Contract.Requires<ArgumentNullException>(model != null);

            var venue = model.ToDomain(ClaimsPrincipal.Current.GetUserId());

            // check for duplicates by location.
            // this is quire naive and could be improved significantly.
            var duplicate = _venueRepository.GetByLocation(venue.Location);
            if (duplicate != null)
                return Conflict();

            _venueRepository.SaveOrUpdate(venue);
            _eventDispatcher.Publish(new VenueCreated
            {
                VenueId = venue.Id,
                Title = venue.Title
            });

            _logger.Information("Created venue {@Venue}.",
                new { venue.Id, venue.Title, venue.CreatedByUser });

            // todo get url from config or something
            return Created("http://api.dev.burgerama.co.uk/venues/" + venue.Id, typeof(Venue));
        }
    }
}
