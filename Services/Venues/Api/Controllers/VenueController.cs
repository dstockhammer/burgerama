using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Venues.Api.Converters;
using Burgerama.Services.Venues.Api.Models;
using Burgerama.Services.Venues.Data.Models;
using Burgerama.Services.Venues.Domain.Contracts;
using Serilog;

namespace Burgerama.Services.Venues.Api.Controllers
{
    public class VenueController : ApiController
    {
        private const string VenueContextKey = "venues";

        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IVenueRepository _venueRepository;

        public VenueController(ILogger logger, IEventDispatcher eventDispatcher, ICommandDispatcher commandDispatcher, IVenueRepository venueRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _commandDispatcher = commandDispatcher;
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
        public IHttpActionResult QueryVenues([FromUri]VenueQuery query)
        {
            var venues = _venueRepository.Find(query)
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
        /// POST /venue
        /// </example>
        /// <param name="model">The venue that should be added.</param>
        /// <returns>Returns the location of the newly added venue.</returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddVenue(VenueModel model)
        {
            Contract.Requires<ArgumentNullException>(model != null);
            
            var venue = model.ToDomain(ClaimsPrincipal.Current.GetUserId(), DateTime.Now);

            // check for duplicates by location.
            // todo: this is quite naive and can be improved significantly.
            var duplicate = _venueRepository.GetByLocation(venue.Location);
            if (duplicate != null)
                return Conflict();

            _venueRepository.SaveOrUpdate(venue);
            _eventDispatcher.Publish(new VenueCreated
            {
                VenueId = venue.Id,
                Title = venue.Name
            });

            _commandDispatcher.Send(new Messaging.Commands.Ratings.CreateCandidate
            {
                ContextKey = VenueContextKey,
                Reference = venue.Id
            });

            _commandDispatcher.Send(new Messaging.Commands.Voting.CreateCandidate
            {
                ContextKey = VenueContextKey,
                Reference = venue.Id,
                OpeningDate = venue.CreatedOn
            });

            _logger.Information("Created venue {@Venue}.",
                new { venue.Id, Title = venue.Name, venue.CreatedByUser });

            var uri = new Uri(Url.Content(string.Format("~/{0}", venue.Id)));
            return Created(uri, venue.ToModel());
        }
    }
}
