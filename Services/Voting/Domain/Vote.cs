using System;
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
    }
}
