using System.Collections.Generic;
using System.Web.Http.Description;
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
    public class RatingController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICandidateRepository _candidateRepository;

        public RatingController(ILogger logger, IEventDispatcher eventDispatcher, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _candidateRepository = candidateRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("{context}/{reference}")]
        [ResponseType(typeof(IEnumerable<RatingModel>))]
        public IHttpActionResult AddNewRatingToVenue(string contextKey, Guid reference, [FromBody]RatingModel model)
        {
            var candidate = _candidateRepository.Get(reference, contextKey);
            if (candidate == null)
            {
                // 1. create potential candidate
                // 2. add rating to that candidate
                // 3. raise TriedToRateUnknownCandidate
                // (4. wait for create candidate command AND expire potential candidates after x seconds/minutes)

                return Ok();
            }

            var userId = ClaimsPrincipal.Current.GetUserId();
            if (candidate.Ratings.Any(r => r.UserId == userId))
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "This user already rated this venue.");

            var events = candidate.AddRating(new Rating(userId, model.Rating, model.Text ?? string.Empty));
            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish(events);
            _logger.Information("Added rating of {Rating} to venue \"{Reference}\".", model.Rating, reference);

            var uri = new Uri(this.Url.Content(string.Format("~/{0}/{1}", contextKey, reference)));
            return Created(uri, candidate.Ratings.Select(v => v.ToModel()));
        }
    }
}