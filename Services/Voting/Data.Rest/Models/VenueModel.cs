using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.Rest.Models
{
    internal class VenueModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime? LatestOuting { get; set; }

        public ICollection<string> Votes { get; set; }
    }
}
