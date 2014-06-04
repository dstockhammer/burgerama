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
        public static VenueModel ToModel(this Candidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);

            var userId = ClaimsPrincipal.Current.GetUserId();

            return new VenueModel
            {
                Id = candidate.Reference.ToString(),
                Rating = candidate.Ratings.Average(r => r.Value),
                CanUserRate = candidate.Ratings.All(r => r.User != userId) 
            };
        }
    }
}