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
using Serilog;

namespace Burgerama.Services.Ratings.Api.Controllers
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
            _logger.Information("Added rating of {Rating} to venue \"{Id}\".", model.Rating, venueId);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}