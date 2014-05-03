using System;

namespace Burgerama.Services.OutingScheduler.Data.Rest.Models
{
    internal sealed class OutingModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public string Venue { get; set; }
    }
}
