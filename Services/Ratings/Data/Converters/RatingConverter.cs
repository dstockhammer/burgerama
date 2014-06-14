using Burgerama.Services.Ratings.Data.Models;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Data.Converters
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

        public static Rating ToDomain(this RatingModel rating)
        {
            if (rating == null)
                return null;

            return new Rating(rating.CreatedOn, rating.UserId, rating.Value, rating.Text);
        }
    }
}
