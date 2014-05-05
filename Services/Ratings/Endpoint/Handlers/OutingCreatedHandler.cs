using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Ratings.Domain.Contracts;
using NServiceBus;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class OutingCreatedHandler : IHandleMessages<OutingCreated>
    {
        private readonly IVenueRepository _venueRepository;

        public OutingCreatedHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public void Handle(OutingCreated message)
        {
            var venue = _venueRepository.Get(message.VenueId);
            venue.AddOuting(message.DateTime);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
