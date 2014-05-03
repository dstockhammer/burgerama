using Burgerama.Common.Authentication.Identity;
using Burgerama.Services.Ratings.Api.Converters;
using Burgerama.Services.Ratings.Api.Models;
using Burgerama.Services.Ratings.Core;
using Burgerama.Services.Ratings.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace Burgerama.Services.Ratings.Api.Controllers
{
    public class VenueController : ApiController
    {
        private readonly IVenueRepository _venueRepository;

        public VenueController(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof (IEnumerable<VenueModel>))]
        public IHttpActionResult GetAllVenues()
        {
            var venue = _venueRepository.GetAll();

            if (venue == null)
                return NotFound();

            return Ok(venue.Select(v => v.ToModel()));
        }


        //[Authorize]
        [HttpGet]
        [Route("{venueId}")]
        [ResponseType(typeof (VenueModel))]
        public IHttpActionResult GetVenueById(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            if (venue == null)
                return NotFound();

            return Ok(venue.ToModel());
        }

        [Authorize]
        [HttpPost]
        [Route("{venueId}")]
        public HttpResponseMessage AddNewRatingToVenue(Guid venueId, [FromBody]RatingModel ratingModel)
        {
            var venue = _venueRepository.Get(venueId) ?? new Venue(venueId);
            if (venue.Ratings.All(r => r.User != ClaimsPrincipal.Current.GetUserId()))
            {
                venue.Ratings.Add(new Rating(ratingModel.User, ratingModel.Rating));
                _venueRepository.SaveOrUpdate(venue);
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "This user already rated this venue.");
        }
    }
}