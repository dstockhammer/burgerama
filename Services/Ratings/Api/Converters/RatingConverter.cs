using Burgerama.Services.Ratings.Api.Models;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Api.Converters
{
    internal static class RatingConverter
    {
        public static RatingModel ToModel(this Rating rating)
        {
            if (rating == null)
                return null;

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