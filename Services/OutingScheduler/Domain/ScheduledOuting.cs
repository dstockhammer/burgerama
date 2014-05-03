using System;

namespace Burgerama.Services.OutingScheduler.Domain
{
    public sealed class ScheduledOuting
    {
        public Guid Id { get; private set; }

        public DateTime Date { get; private set; }

        public Venue Venue { get; private set; }

        public ScheduledOuting(DateTime date, Venue venue)
        {
            Id = Guid.NewGuid();
            Date = date;
            Venue = venue;
        }
    }
}
