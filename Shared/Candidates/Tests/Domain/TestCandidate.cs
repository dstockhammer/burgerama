using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Shared.Candidates.Domain;

namespace Burgerama.Shared.Candidates.Tests.Domain
{
    internal sealed class TestCandidate : Candidate<TestItem>
    {
        public TestCandidate(string contextKey, Guid reference)
            : base(contextKey, reference)
        {
        }

        public TestCandidate(string contextKey, Guid reference, IEnumerable<TestItem> items, DateTime? openingDate = null, DateTime? closingDate = null)
            : base(contextKey, reference, items, openingDate, closingDate)
        {
        }

        public override IEnumerable<IEvent> AddItem(TestItem item)
        {
            _items.Add(item);

            return Enumerable.Empty<IEvent>();
        }
    }
}
