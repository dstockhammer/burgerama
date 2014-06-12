using Burgerama.Services.Ratings.Api.Models;
using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Api.Converters
{
    internal static class RatingConverter
    {
        public static RatingModel ToModel(this Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);

            // todo:
            //var userId = ClaimsPrincipal.Current.GetUserId();
            //CanUserRate = candidate.Ratings.All(r => r.User != userId)

            return new RatingModel
            {
                CreatedOn = rating.CreatedOn,
                UserId = rating.UserId,
                Value = rating.Value,
                Text = rating.Text
            };
        }
    }
}