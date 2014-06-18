using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Voting;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class TriedToVoteUnknownCandidateHandler : Consumes<TriedToVoteUnknownCandidate>.Context
    {
        private readonly IVenueRepository _venueRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public TriedToVoteUnknownCandidateHandler(
            IVenueRepository venueRepository, 
            ICommandDispatcher commandDispatcher)
        {
            _venueRepository = venueRepository;
            _commandDispatcher = commandDispatcher;
        }

        public void Consume (IConsumeContext<TriedToVoteUnknownCandidate> context)
        {
            if (context.Message.ContextKey.Contains("venue"))
            {
                var venue = _venueRepository.Get(context.Message.Reference);

                if (venue != null)
                {
                    _commandDispatcher.Send(new CreateCandidate
                    {
                        ContextKey = context.Message.ContextKey,
                        Reference = context.Message.Reference.ToString(),
                        UserId = context.Message.UserId,
                        VotedOn = context.Message.VotedOn
                    });
                }
            }
        }
    }
}
