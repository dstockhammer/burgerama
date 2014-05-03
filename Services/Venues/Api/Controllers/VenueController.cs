using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Services.Venues.Api.Converters;
using Burgerama.Services.Venues.Api.Models;
using Burgerama.Services.Venues.Domain;
using Burgerama.Services.Venues.Domain.Contracts;

namespace Burgerama.Services.Venues.Api.Controllers
{
    public class VenueController : ApiController
    {
        private readonly IVenueRepository _venueRepository;

        public VenueController(IVenueRepository venueRepository, IEventHandler )
        {
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

            var userId = ClaimsPrincipal.Current.GetUserId();
            var location = new Location(model.Location.Reference, model.Location.Latitiude, model.Location.Longitude);
            var venue = new Venue(model.Title, location, userId)
            {
                Description = model.Description,
                Url = model.Url
            };

            // todo: dupe check!
            //if (isDupe) 
            //    return Conflict();

            _venueRepository.SaveOrUpdate(venue);

            // todo get url from config or something
            return Created("http://api.dev.burgerama.co.uk/venues/" + venue.Id, typeof(Venue));
        }
    }
}
