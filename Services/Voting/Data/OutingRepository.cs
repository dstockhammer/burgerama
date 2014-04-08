using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Services.Voting.Core.Data;
using Burgerama.Services.Voting.Core.Domain;

namespace Burgerama.Services.Voting.Data
{
    public class OutingRepository : IOutingRepository
    {
        private readonly ICollection<Outing> _outings = new List<Outing>();

        public bool Any(Guid venueId)
        {
            return _outings.Any(o => o.Venue == venueId);
        }

        public void Add(Guid venueId)
        {
            // allow only a single outing per venue
            if (_outings.Any(o => o.Venue == venueId))
                return;

            _outings.Add(new Outing(venueId));
        }
    }
}
