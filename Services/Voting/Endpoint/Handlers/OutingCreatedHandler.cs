using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class OutingCreatedHandler : Consumes<OutingCreated>.Context
    {
        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;
        
        public OutingCreatedHandler(ILogger logger, IVenueRepository venueRepository)
        {
            _logger = logger;
            _venueRepository = venueRepository;
        }

        public void Consume(IConsumeContext<OutingCreated> context)
        {
            var venue = _venueRepository.Get(context.Message.VenueId);
            if (venue == null)
            {
                // todo: call the venue service to check whether a venue with this guid exists:
                // if yes: a VenueCreated event was lost -> create a venue with this id
                // if no: OutingCreated is corrupted -> fail
                // for now, just assume there was an previous error and just create a new venue.

                venue = new Venue(context.Message.VenueId, string.Empty);

                _logger.Warning("Outing \"{OutingId}\" was created for unknown venue \"{VenueId}\". Venue was created.",
                    new { context.Message.OutingId, context.Message.VenueId });
            }

            venue.AddOuting(context.Message.DateTime);
            _venueRepository.SaveOrUpdate(venue);

            _logger.Information("Outing \"{OutingId}\" added to venue \"{VenueId}\".",
                new { context.Message.OutingId, context.Message.VenueId });
        }
    }
}
