using System;
using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using NServiceBus;

namespace Burgerama.Services.Voting.Endpoint.Handlers
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
            if (venue == null)
            {
                // call the venue service to check whether a venue with this guid exists:
                // if yes: a VenueCreated event was lost -> create a venue with this id
                // if no: OutingCreated is corrupted -> fail

                venue = new Venue(Guid.Empty);
            }

            venue.AddOuting(message.OutingId);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
