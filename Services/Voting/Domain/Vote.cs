using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Vote
    {
        public DateTime CreatedOn { get; private set; }

        public string UserId { get; private set; }

        public Vote(DateTime createdOn, string userId)
        {
            Contract.Requires<ArgumentNullException>(userId != null);

            CreatedOn = createdOn;
            UserId = userId;
        }

        private sealed class UserEqualityComparer : IEqualityComparer<Vote>
        {
            public bool Equals(Vote x, Vote y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.UserId, y.UserId);
            }

            public int GetHashCode(Vote obj)
            {
                return (obj.UserId != null ? obj.UserId.GetHashCode() : 0);
            }
        }

        private static readonly IEqualityComparer<Vote> UserComparerInstance = new UserEqualityComparer();

        public static IEqualityComparer<Vote> UserComparer
        {
            get { return UserComparerInstance; }
        }
    }
}
