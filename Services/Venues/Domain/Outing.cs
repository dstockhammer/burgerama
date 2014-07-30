using System;

namespace Burgerama.Services.Venues.Domain
{
    public sealed class Outing
    {
        public Guid Id { get; private set; }

        public DateTime Date { get; private set; }

        public Outing(Guid id, DateTime date)
        {
            Date = date;
            Id = id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Outing;
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
