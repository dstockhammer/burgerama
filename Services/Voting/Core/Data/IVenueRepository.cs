using System;
using System.Collections.Generic;
using Burgerama.Services.Voting.Domain;

namespace Burgerama.Services.Voting.Core.Data
{
    public interface IVenueRepository
    {
        Venue Get(Guid venueId);

        IEnumerable<Venue> GetVotesForUser(Guid userId);

        void SaveOrUpdate(Venue venue);
    }
}
