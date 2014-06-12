using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class TriedToRateUnknownCandidateHandler : Consumes<TriedToRateUnknownCandidate>.Context
    {
        private const string VenueContextKey = "venues";

        private readonly ILogger _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IVenueRepository _venueRepository;

        public TriedToRateUnknownCandidateHandler(ILogger logger, ICommandDispatcher commandDispatcher, IVenueRepository venueRepository)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _venueRepository = venueRepository;
        }

        public void Consume(IConsumeContext<TriedToRateUnknownCandidate> context)
        {
            // ignore events that don't belong to the venue context
            if (context.Message.ContextKey != VenueContextKey)
                return;

            var venue = _venueRepository.Get(context.Message.Reference);
            if (venue != null)
            {
                _commandDispatcher.Send(new CreateCandidate
                {
                    ContextKey = VenueContextKey,
                    Reference = venue.Id
                });

                _logger.Information("Created rating candidate for venue \"{Reference}\".",
                    new { context.Message.Reference });
            }
        }
    }
}
