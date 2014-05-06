using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
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
            var venue = new Venue(context.Message.VenueId, context.Message.Title);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
