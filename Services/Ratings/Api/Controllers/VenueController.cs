using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Api.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;

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
            var venue = _venueRepository.Get(venueId);
            if (venue == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Venue not found.");

            var userId = ClaimsPrincipal.Current.GetUserId();
            if (venue.Ratings.Any(r => r.User == userId))
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "This user already rated this venue.");

            var events = venue.AddRating(new Rating(userId, model.Rating, model.Text ?? string.Empty));
            _venueRepository.SaveOrUpdate(venue);
            _eventDispatcher.Publish(events);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}