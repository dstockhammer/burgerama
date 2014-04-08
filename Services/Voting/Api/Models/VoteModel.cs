using System;

namespace Burgerama.Services.Voting.Api.Models
{
    public class VoteModel
    {
        public Guid User { get; set; }

        public Guid Venue { get; set; }
    }
}