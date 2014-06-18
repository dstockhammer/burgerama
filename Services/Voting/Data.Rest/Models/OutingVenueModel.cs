
using System;

namespace Burgerama.Services.Voting.Data.Rest.Models
{
    internal class OutingVenueModel
    {
        public string Id { get; set; }

        public DateTime? LatestOuting { get; set; }
    }
}
