using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Ratings.Domain.Contracts;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class OutingCreatedHandler : Consumes<OutingCreated>.Context
    {
        private readonly IVenueRepository _venueRepository;

        public OutingCreatedHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public void Consume(IConsumeContext<OutingCreated> context)
        {
            var venue = _venueRepository.Get(context.Message.VenueId);
            venue.AddOuting(context.Message.DateTime);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
