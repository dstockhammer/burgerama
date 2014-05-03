using Burgerama.Services.Ratings.Core;
using Burgerama.Services.Ratings.Data.Models;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Data.Converters
{
    internal static class RatingConverter
    {
        public static RatingModel ToModel(this Rating rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);

            return new RatingModel
            {
                User = rating.User,
                Value = rating.Value
            };
        }

        public static Rating ToDomain(this RatingModel rating)
        {
            Contract.Requires<ArgumentNullException>(rating != null);

            return new Rating(rating.User, rating.Value);
        }
    }
}
