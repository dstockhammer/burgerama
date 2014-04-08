using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Core.Data;
using Burgerama.Services.Voting.Core.Domain;

namespace Burgerama.Services.Voting.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class VoteController : ApiController
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IOutingRepository _outingRepository;

        public VoteController(IVoteRepository voteRepository, IOutingRepository outingRepository)
        {
            _voteRepository = voteRepository;
            _outingRepository = outingRepository;
        }

        /// <summary>
        /// Returns the total number of votes for a venue.
        /// </summary>
        /// <example>
        /// GET venue/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="venueId">The guid of the venue.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("venue/{venueId}")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetVotesForVenue(Guid venueId)
        {
            return Ok(3);
        }

        /// <summary>
        /// Returns the detailed votes for a venue.
        /// </summary>
        /// <example>
        /// GET venue/878f000c-e61f-4d34-a9f7-236a153c062c/details
        /// </example> 
        /// <param name="venueId">The guid of the venue.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("venue/{venueId}/details")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult GetVoteDetailsForVenue(Guid venueId)
        {
            return Ok(new[]
            {
                new VoteModel
                {
                    User = Guid.NewGuid(),
                    Venue = venueId
                },
                new VoteModel
                {
                    User = Guid.NewGuid(),
                    Venue = venueId
                },
                new VoteModel
                {
                    User = Guid.NewGuid(),
                    Venue = venueId
                }
            });
        }

        /// <summary>
        /// Returns the number of votes for a venue.
        /// </summary>
        /// <example>
        /// GET user/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="userId">The guid of the user.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/{userId}")]
        [ResponseType(typeof(IEnumerable<VoteModel>))]
        public IHttpActionResult GetVotesByUser(Guid userId)
        {
            return Ok(new[]
            {
                new VoteModel
                {
                    User = userId,
                    Venue = Guid.NewGuid()
                },
                new VoteModel
                {
                    User = userId,
                    Venue = Guid.NewGuid()
                },
                new VoteModel
                {
                    User = userId,
                    Venue = Guid.NewGuid()
                }
            });
        }

        /// <summary>
        /// Adds a vote to a venue.
        /// </summary>
        /// <example>
        /// POST api/venue/878f000c-e61f-4d34-a9f7-236a153c062c
        /// </example> 
        /// <param name="venueId"></param>
        [HttpPost]
        [Route("venue/{venueId}")]
        public IHttpActionResult AddVote(Guid venueId)
        {
            // if there is an outing for this venue, voting is not allowed
            if (_outingRepository.Any(venueId))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "An outing already exists for this venue."
                });
            }

            // check if user has already voted
            _voteRepository.Add(new Vote(Guid.NewGuid(), venueId));

            // raise event

            return Ok();
        }
    }
}