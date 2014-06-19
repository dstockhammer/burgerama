using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Domain
{
    public sealed class Rating
    {
        public DateTime CreatedOn { get; private set; }

        public string UserId { get; private set; }

        public double Value { get; private set; }

        public string Text { get; private set; }

        public Rating(DateTime createdOn, string userId, double value, string text)
        {
            Contract.Requires<ArgumentNullException>(userId != null);
            Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 1);

            CreatedOn = createdOn;
            UserId = userId;
            Value = value;
            Text = text;
        }
    }
}
