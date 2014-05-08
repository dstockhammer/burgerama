using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MassTransit;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class VenueCreatedHandler : Consumes<VenueCreated>.Context
    {
        private readonly IVenueRepository _venueRepository;

        public VenueCreatedHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public void Consume(IConsumeContext<VenueCreated> context)
        {
            var venue = new Venue(context.Message.VenueId);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
