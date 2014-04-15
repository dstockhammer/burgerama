using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Services.Venues.Api.Models;
using Burgerama.Services.Venues.Core.Data;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Api.Controllers
{
    public class VenueController : ApiController
    {
        private readonly IVenueRepository _venueRepository;

        public VenueController(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        /// <summary>
        /// Get all venues.
        /// </summary>
        /// <remarks>
        /// todo: paging, sorting, filtering
        /// </remarks>
        /// <example>
        /// GET venue
        /// </example> 
        /// <returns>Returns all venues.</returns>
        [HttpGet]
        [Route("venues")]
        [ResponseType(typeof(IEnumerable<VenueModel>))]
        public IHttpActionResult GetAllVenues()
        {
            //var venues =  _venueRepository.GetAll()
            //    .Select(v => new VenueModel
            //    {
            //        Id = v.Id.ToString(),
            //        Title = v.Title,
            //        Location = v.Location,
            //        Url = v.Url,
            //        Description = v.Description,
            //        Rating = 0,
            //        Votes = 0
            //    });

            var venues = new[]
            {
                new VenueModel { Id = Guid.NewGuid().ToString(), Title = "Venue 1" },
                new VenueModel { Id = Guid.NewGuid().ToString(), Title = "Venue 2" },
                new VenueModel { Id = Guid.NewGuid().ToString(), Title = "Venue 3" }
            };

            return Ok(venues);
        }

        /// <summary>
        /// Get a venue by id.
        /// </summary>
        /// <example>
        /// GET venue 878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="venueId">The guid of the venue.</param>
        /// <returns>Returns the venue with the passed id.</returns>
        [HttpGet]
        [Route("venues/{venueId}")]
        [ResponseType(typeof(VenueModel))]
        public IHttpActionResult GetVenueById(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            if (venue == null)
                return NotFound();

            return Ok(new VenueModel
            {
                Id = venue.Id.ToString(),
                Title = venue.Title,
                Location = venue.Location,
                Url = venue.Url,
                Description = venue.Description,
                Rating = 0,
                Votes = 0
            });
        }

        [Authorize]
        [HttpPost]
        [Route("venues")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult AddVenue(VenueModel model)
        {
            // todo: get userId from claims
            var claims = ClaimsPrincipal.Current.Claims;
            var userId = Guid.NewGuid();

            var venue = new Venue(model.Title, model.Location, userId);

            // todo: dupe check!
            //if (isDupe) 
            //    return Conflict();

            _venueRepository.SaveOrUpdate(venue);

            return Created("/api/venues/" + venue.Id, typeof(Venue));
        }
    }
}