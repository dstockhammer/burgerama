using System.ComponentModel.DataAnnotations;
using Burgerama.Services.Venues.Domain;

namespace Burgerama.Services.Venues.Api.Models
{
    public sealed class VenueModel
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Location Location { get; set; }
        
        public string Url { get; set; }

        public string Description { get; set; }

        public int Votes { get; set; }

        public double Rating { get; set; }
    }
}