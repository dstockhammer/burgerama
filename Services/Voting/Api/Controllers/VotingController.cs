using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Services.Voting.Core.Data;

namespace Burgerama.Services.Voting.Api.Controllers
{
    //[Authorize]
    public class VotingController : ApiController
    {
        private readonly IVenueRepository _venueRepository;

        public VotingController(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
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
        [Route("venue/{venueId}")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetVotesForVenue(Guid venueId)
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
        [Route("venue/{venueId}/details")]
        [ResponseType(typeof(IEnumerable<Guid>))]
        public IHttpActionResult GetVoteDetailsForVenue(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            if (venue == null)
                return NotFound();

            return Ok(venue.Votes);
        }

        /// <summary>
        /// Get the vote details for a user.
        /// </summary>
        /// <example>
        /// GET user/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="userId">The guid of the user.</param>
        /// <returns>Returns the ids of all venues the user has voted for.</returns>
        [HttpGet]
        [Route("user/{userId}")]
        [ResponseType(typeof(IEnumerable<Guid>))]
        public IHttpActionResult GetVotesByUser(Guid userId)
        {
            var venues = _venueRepository.GetVotesForUser(userId);

            return Ok(venues.Select(v => v.Id));
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
        [ResponseType(typeof(bool))]
        public IHttpActionResult AddVote(Guid venueId)
        {
            var venue = _venueRepository.Get(venueId);

            // todo: get the userid from identity.
            var userId = Guid.NewGuid();

            var voteAdded = venue.AddVote(userId);
            if (voteAdded == false) 
                return Conflict();

            _venueRepository.SaveOrUpdate(venue);

            return Ok();
        }
    }
}