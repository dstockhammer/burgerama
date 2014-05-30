using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Candidate
    {
        public Guid Reference { get; private set; }

        public IEnumerable<Vote> Votes { get; private set; }

        public DateTime? Expiry { get; private set; }

        public Candidate(Guid reference, IEnumerable<Vote> votes, DateTime? expiry = null)
        {
            Reference = reference;
            Votes = votes;
            Expiry = expiry;
        }

        public void AddVote(string createdBy, DateTime createdOn)
        {
            if (this.Expiry < createdOn)
            {
                
            }
        }

        public void ExpireOn(DateTime dateTime)
        {
            this.Expiry = dateTime;
        }
    }
}
