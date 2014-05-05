using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class VenueCreatedHandler // : IHandleMessages<VenueCreated>
    {
        private readonly IVenueRepository _venueRepository;

        public VenueCreatedHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public void Handle(VenueCreated message)
        {
            var venue = new Venue(message.VenueId, message.Title);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
