using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class VenueCreatedHandler : Consumes<VenueCreated>.Context
    {
        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;

        public VenueCreatedHandler(ILogger logger, IVenueRepository venueRepository)
        {
            _logger = logger;
            _venueRepository = venueRepository;
        }

        public void Consume(IConsumeContext<VenueCreated> context)
        {
            var venue = new Venue(context.Message.VenueId, context.Message.Title);
            _venueRepository.SaveOrUpdate(venue);

            _logger.Information("Venue \"{VenueId}\" created.",
                new { context.Message.VenueId, context.Message.Title });
        }
    }
}
