using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Description;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Ratings.Api.Converters;
using Burgerama.Services.Ratings.Api.Models;
using System;
using System.Linq;
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
        private readonly IContextRepository _contextRepository;
        private readonly ICandidateRepository _candidateRepository;

        public RatingController(ILogger logger, IEventDispatcher eventDispatcher, IContextRepository contextRepository, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _contextRepository = contextRepository;
            _candidateRepository = candidateRepository;
        }

        [HttpGet]
        [Route("{contextKey}/{reference}")]
        [ResponseType(typeof(IEnumerable<RatingModel>))]
        public IHttpActionResult GetRatingSummaryForCandidate(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            var model = new CandidateModel
            {
                ContextKey = contextKey,
                Reference = reference
            };

            // first, check all real (= well known) candidates
            var candidate = _candidateRepository.Get(contextKey, reference);
            if (candidate != null)
            {
                model.IsValidated = true;
                model.OpeningDate = candidate.OpeningDate;
                model.ClosingDate = candidate.ClosingDate;
                model.RatingsCount = candidate.Ratings.Count();
                model.TotalRating = candidate.TotalRating;
                model.CanUserRate = candidate.CanUserRate(ClaimsPrincipal.Current.GetUserId());

                return Ok(model);
            }
            
            // if there is no real candidate, check the potential ones
            var potentialCandidate = _candidateRepository.GetPotential(contextKey, reference);
            if (potentialCandidate != null)
            {
                model.IsValidated = false;
                model.RatingsCount = potentialCandidate.Ratings.Count();
                model.TotalRating = potentialCandidate.TotalRating;
                model.CanUserRate = true;

                return Ok(model);
            }

            // if no candidate is found at all, check if the context is valid
            var context = _contextRepository.Get(contextKey);
            if (context == null)
                return BadRequest("Invalid context.");

            // if the context is valid, still return an empty, not validated model in order to allow users to rate
            model.IsValidated = false;
            model.CanUserRate = true;

            return Ok(model);
        }

        [HttpGet]
        [Route("{contextKey}/{reference}/ratings")]
        [ResponseType(typeof(IEnumerable<RatingModel>))]
        public IHttpActionResult GetAllRatingsForCandidate(string contextKey, Guid reference)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);

            // first, check all real (= well known) candidates
            var candidate = _candidateRepository.Get(contextKey, reference);
            if (candidate != null)
                return Ok(candidate.Ratings.Select(r => r.ToModel()));

            // if there is no real candidate, check the potential ones
            var potentialCandidate = _candidateRepository.GetPotential(contextKey, reference);
            if (potentialCandidate != null)
                Ok(potentialCandidate.Ratings.Select(r => r.ToModel()));

            // if no candidate is found at all, check if the context is valid,
            // in order to determine the correct error type
            var context = _contextRepository.Get(contextKey);
            if (context == null)
                return BadRequest("Invalid context.");

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        [Route("{contextKey}/{reference}/ratings")]
        [ResponseType(typeof(IEnumerable<RatingModel>))]
        public IHttpActionResult AddRatingToCandidate(string contextKey, Guid reference, [FromBody]RatingModel model)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(model != null);

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var userId = ClaimsPrincipal.Current.GetUserId();

            // todo: validate rating?
            var rating = new Rating(DateTime.Now, userId, model.Value, model.Text);

            var candidate = _candidateRepository.Get(contextKey, reference);
            if (candidate != null)
            {
                if (candidate.Ratings.Any(r => r.UserId == userId))
                    return Conflict();

                var events = candidate.AddRating(new Rating(DateTime.Now, userId, model.Value, model.Text ?? string.Empty));

                _candidateRepository.SaveOrUpdate(candidate);
                _eventDispatcher.Publish(events);

                _logger.Information("Added rating of {Rating} to candidate {Reference}.",
                    model.Value, reference);

                var candidateUri = new Uri(Url.Content(string.Format("~/{0}/{1}", contextKey, reference)));
                return Created(candidateUri, rating.ToModel());
            }

            var potentialCandidate = _candidateRepository.GetPotential(contextKey, reference);
            if (potentialCandidate == null)
            {
                // if there isn't already a potential candidate, validate the context
                var context = _contextRepository.Get(contextKey);
                if (context == null)
                    return BadRequest("Invalid context.");

                // if the context is valid, use it to determine whether rating an unknown candidate is allowed
                if (context.AllowToRateUnknownCandidates == false)
                    return NotFound();

                potentialCandidate = new PotentialCandidate(contextKey, reference);
            }

            if (potentialCandidate.Ratings.Any(r => r.UserId == userId))
                return Conflict();

            potentialCandidate.AddRating(rating);

            _candidateRepository.SaveOrUpdate(potentialCandidate);
            _eventDispatcher.Publish(new TriedToRateUnknownCandidate
            {
                ContextKey = contextKey,
                Reference = reference
            });

            _logger.Information("Added rating of {Rating} to unknown candidate {Reference}.",
                model.Value, reference);

            var uri = new Uri(Url.Content(string.Format("~/{0}/{1}", contextKey, reference)));
            return Created(uri, rating.ToModel());
        }
    }
}
