using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Common.Authentication.Identity;
using Burgerama.Messaging.Events;
using Burgerama.Services.Voting.Domain.Contracts;

namespace Burgerama.Services.Voting.Api.Controllers
{
    [Authorize]
    public class VotingController : ApiController
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public VotingController(IVenueRepository venueRepository, IEventDispatcher eventDispatcher)
        {
            _venueRepository = venueRepository;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Get the total number of votes for a venue.
        /// </summary>
        /// <example>
        /// GET venue/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="venueId">The guid of the venue.</param>
        /// <returns>Returns the total number of votes for the venue.</returns>
        [HttpGet]
        [Route("venue/{venueId}/count")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetVoteCountForVenue(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            if (venue == null)
                return NotFound();

            return Ok(venue.Votes.Count());
        }

        /// <summary>
        /// Get the vote details for a venue.
        /// </summary>
        /// <example>
        /// GET venue/878f000c-e61f-4d34-a9f7-236a153c062c/details
        /// </example> 
        /// <param name="venueId">The guid of the venue.</param>
        /// <returns>Returns the ids of all users who have voted for the venue.</returns>
        [HttpGet]
        [Route("venue/{venueId}")]
        [ResponseType(typeof(IEnumerable<Guid>))]
        public IHttpActionResult GetVotesForVenue(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            if (venue == null)
                return NotFound();

            return Ok(venue.Votes);
        }

        /// <summary>
        /// Add a vote to a venue.
        /// </summary>
        /// <example>
        /// POST api/venue/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="venueId">The guid of the venue.</param>
        [HttpPost]
        [Route("venue/{venueId}")]
        public IHttpActionResult AddVote(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);
            if (venue == null)
                return NotFound();

            var userId = ClaimsPrincipal.Current.GetUserId();
            var messages = venue.AddVote(userId);
            if (messages.Any() == false)
                return Conflict();

            _venueRepository.SaveOrUpdate(venue);
            _eventDispatcher.Publish(messages);

            return Ok();
        }

        /// <summary>
        /// Get the vote details for a user.
        /// </summary>
        /// <example>
        /// GET user/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="userId">The id of the user.</param>
        /// <returns>Returns the ids of all venues the user has voted for.</returns>
        [HttpGet]
        [Route("user/{userId}")]
        [ResponseType(typeof(IEnumerable<Guid>))]
        public IHttpActionResult GetVotesByUser(string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);

            var venues = _venueRepository.GetVotesForUser(userId);
            return Ok(venues.Select(v => v.Id));
        }
    }
}
