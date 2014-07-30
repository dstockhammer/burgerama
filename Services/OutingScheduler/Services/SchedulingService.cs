using System;
using System.Linq;
using Burgerama.Services.OutingScheduler.Domain;
using Burgerama.Services.OutingScheduler.Domain.Contracts;

namespace Burgerama.Services.OutingScheduler.Services
{
    public sealed class SchedulingService : ISchedulingService
    {
        private readonly IOutingRepository _outingRepository;
        private readonly IVenueRepository _venueRepository;

        public SchedulingService(IOutingRepository outingRepository, IVenueRepository venueRepository)
        {
            _outingRepository = outingRepository;
            _venueRepository = venueRepository;
        }

        public ScheduledOuting ScheduleOuting(DateTime date)
        {
            var venue = DetermineVenue();
            if (venue == null)
                return null;

            return new ScheduledOuting(date, venue);
        }

        /// <summary>
        /// Determines the venue for the next outing.
        /// Finds all venues without an outing and choose the venue with the 
        /// highest number of votes. If multiple matches are found, a venue is
        /// chosen randomly.
        /// </summary>
        private Venue DetermineVenue()
        {
            var venues = _venueRepository.GetAll();
            var outings = _outingRepository.GetAll();

            var potentialVenues = venues.Where(v => outings.Any(o => o.VenueId == v.Id) == false).ToList();
            if (potentialVenues.Any() == false)
                return null;

            return potentialVenues.OrderByDescending(v => v.TotalVotes).First();
        }
    }
}
