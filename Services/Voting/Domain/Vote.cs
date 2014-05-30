using System;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Vote
    {
        public DateTime CreatedOn { get; private set; }

        public string CreatedBy { get; private set; }

        public Vote(DateTime createdOn, string createdBy)
        {
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }
    }
}
