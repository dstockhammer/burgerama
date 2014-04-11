using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Voting.Core.Data;
using Burgerama.Services.Voting.Domain;
using NServiceBus;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class VenueCreatedHandler : IHandleMessages<VenueCreated>
    {
        private readonly IVenueRepository _venueRepository;

        public VenueCreatedHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public void Handle(VenueCreated message)
        {
            var venue = new Venue(message.VenueId);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
