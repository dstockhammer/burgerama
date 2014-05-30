using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Candidate
    {
        public Guid Reference { get; private set; }

        public IEnumerable<Vote> Votes { get; private set; }

        public DateTime? Expiry { get; private set; }


        public Candidate(Guid reference, DateTime? expiry = null) : this(reference, new List<Vote>(), expiry) { }

        public Candidate(Guid reference, IEnumerable<Vote> votes, DateTime? expiry = null)
        {
            Reference = reference;
            Votes = votes;
            Expiry = expiry;
        }

        public void Vote(string userId, DateTime votedOn)
        {
            if (this.Expiry != null && this.Expiry < votedOn)
            {
                this.Votes = new List<Vote>(this.Votes) {new Vote(votedOn, userId)};
            }
        }

        public void ExpireOn(DateTime dateTime)
        {
            this.Expiry = dateTime;
        }
    }
}
