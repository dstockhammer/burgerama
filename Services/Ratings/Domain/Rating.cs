
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Core
{
    public sealed class Rating
    {
        public string User { get; private set; }

        public double Value { get; private set; }

        public string Text { get; private set; }

        public Rating(string user, double value, string text)
        {
            Contract.Requires<ArgumentNullException>(user != null);
            Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 1);
            Contract.Requires<ArgumentNullException>(text != null);

            User = user;
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
                return string.Equals(x.User, y.User);
            }

            public int GetHashCode(Rating obj)
            {
                return (obj.User != null ? obj.User.GetHashCode() : 0);
            }
        }

        private static readonly IEqualityComparer<Rating> UserComparerInstance = new UserEqualityComparer();

        public static IEqualityComparer<Rating> UserComparer
        {
            get { return UserComparerInstance; }
        }
    }
}
