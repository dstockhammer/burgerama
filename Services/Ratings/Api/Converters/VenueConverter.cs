using Burgerama.Common.Authentication.Identity;
using Burgerama.Services.Ratings.Api.Models;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Api.Converters
{
    internal static class VenueConverter
    {
        public static VenueModel ToModel(this Venue venue)
        {
            Contract.Requires<ArgumentNullException>(venue != null);

            var userId = ClaimsPrincipal.Current.GetUserId();

            return new VenueModel
            {
                Id = venue.Id.ToString(),
                Rating = venue.Ratings.Average(r => r.Value),
                CanUserRate = venue.Ratings.All(r => r.User != userId) 
            };
        }
    }
}