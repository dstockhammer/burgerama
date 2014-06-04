using System;
using System.Collections.Generic;
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
            Contract.Requires<ArgumentNullException>(text != null);

            CreatedOn = createdOn;
            UserId = userId;
            Value = value;
            Text = text;
        }

        private sealed class UserEqualityComparer : IEqualityComparer<Rating>
        {
            public bool Equals(Rating x, Rating y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.UserId, y.UserId);
            }

            public int GetHashCode(Rating obj)
            {
                return (obj.UserId != null ? obj.UserId.GetHashCode() : 0);
            }
        }

        private static readonly IEqualityComparer<Rating> UserComparerInstance = new UserEqualityComparer();

        public static IEqualityComparer<Rating> UserComparer
        {
            get { return UserComparerInstance; }
        }
    }
}
