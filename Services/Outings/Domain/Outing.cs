﻿using System;

namespace Burgerama.Services.Outings.Domain
{
    public sealed class Outing
    {
        public Guid Id { get; private set; }

        public DateTime Date { get; private set; }

        public Guid Venue { get; private set; }

        public Outing(DateTime date, Guid venue)
        {
            Id = Guid.NewGuid();
            Date = date;
            Venue = venue;
        }

        public Outing(Guid id, DateTime date, Guid venue)
        {
            Id = id;
            Date = date;
            Venue = venue;
        }
    }
}
