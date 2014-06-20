using System;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Voting;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class TriedToVoteUnknownCandidateHandler : Consumes<TriedToVoteForUnknownCandidate>.Selected
    {
        private const string VenueContextKey = "venues";

        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public TriedToVoteUnknownCandidateHandler(ILogger logger, IVenueRepository venueRepository, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _venueRepository = venueRepository;
            _commandDispatcher = commandDispatcher;
        }

        public bool Accept(TriedToVoteForUnknownCandidate message)
        {
            return message.ContextKey == VenueContextKey;
        }

        public void Consume(TriedToVoteForUnknownCandidate message)
        {
            var venue = _venueRepository.Get(message.Reference);
            if (venue == null)
                return;

            _commandDispatcher.Send(new CreateCandidate
            {
                ContextKey = message.ContextKey,
                Reference = message.Reference,
                OpeningDate = DateTime.Today // todo: figure out a good way to get the REAL opening date
            });

            _logger.Information("Created rating candidate for venue {Reference}.",
                message.Reference);
        }
    }
}
