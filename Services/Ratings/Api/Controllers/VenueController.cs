using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Api.Models;
using Burgerama.Services.Ratings.Core;
using Burgerama.Services.Ratings.Core.Contracts;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Burgerama.Services.Ratings.Api.Controllers
{
    public class VenueController : ApiController
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public VenueController(IVenueRepository venueRepository, IEventDispatcher eventDispatcher)
        {
            _venueRepository = venueRepository;
            _eventDispatcher = eventDispatcher;
        }

        [Authorize]
        [HttpPost]
        [Route("{venueId}")]
        public HttpResponseMessage AddNewRatingToVenue(Guid venueId, [FromBody]RatingModel model)
        {
            var userId = ClaimsPrincipal.Current.GetUserId();
            
            // todo: instead of naively creating a new venue here, the system should subscribe
            // to VenueCreated events and keep its venues in sync. if somebody wants to add a
            // rating to a unknow venue, it should display 404 or a similar error.
            var venue = _venueRepository.Get(venueId) ?? new Venue(venueId);
            
            if (venue.Ratings.Any(r => r.User == userId))
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "This user already rated this venue.");

            var events = venue.AddRating(new Rating(userId, model.Rating, model.Text ?? string.Empty));
            _venueRepository.SaveOrUpdate(venue);
            _eventDispatcher.Publish(events);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}