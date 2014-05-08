using System;
using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MassTransit;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class OutingCreatedHandler : Consumes<OutingCreated>.Context
    {
        private readonly IVenueRepository _venueRepository;

        public OutingCreatedHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public void Handle(OutingCreated message)
        {

        }

        public void Consume(IConsumeContext<OutingCreated> context)
        {
            var venue = _venueRepository.Get(context.Message.VenueId);
            if (venue == null)
            {
                // call the venue service to check whether a venue with this guid exists:
                // if yes: a VenueCreated event was lost -> create a venue with this id
                // if no: OutingCreated is corrupted -> fail

                venue = new Venue(Guid.Empty);
            }

            venue.AddOuting(context.Message.DateTime);
            _venueRepository.SaveOrUpdate(venue);
        }
    }
}
