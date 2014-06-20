using System;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class TriedToRateUnknownCandidateHandler : Consumes<TriedToRateUnknownCandidate>.Selected
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

        public bool Accept(TriedToRateUnknownCandidate message)
        {
            return message.ContextKey == VenueContextKey;
        }

        public void Consume(TriedToRateUnknownCandidate message)
        {
            var venue = _venueRepository.Get(message.Reference);
            if (venue == null)
                return;

            _commandDispatcher.Send(new CreateCandidate
            {
                ContextKey = VenueContextKey,
                Reference = venue.Id,
                OpeningDate = DateTime.Today // todo: figure out a good way to get the REAL opening date
            });

            _logger.Information("Created rating candidate for venue {Reference}.",
                message.Reference);
        }
    }
}
