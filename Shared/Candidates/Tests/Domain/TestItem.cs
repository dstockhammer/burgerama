using System;

namespace Burgerama.Shared.Candidates.Tests.Domain
{
    internal sealed class TestItem
    {
        public Guid Id { get; private set; }

        public TestItem()
        {
            Id = Guid.NewGuid();
        }

        public TestItem(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as TestItem;
            if (other == null || GetType() != obj.GetType())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + Id.GetHashCode();

                return hash;
            }
        }
    }
}
