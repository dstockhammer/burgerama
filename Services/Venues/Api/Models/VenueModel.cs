using System;

namespace Burgerama.Services.Venues.Api.Models
{
    public sealed class VenueModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Votes { get; set; }

        public double Rating { get; set; }
    }
}