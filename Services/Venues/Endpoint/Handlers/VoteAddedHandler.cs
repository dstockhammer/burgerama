using Burgerama.Messaging.Events.Voting;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class VoteAddedHandler : Consumes<VoteAdded>.Context
    {
        private readonly ILogger _logger;

        public VoteAddedHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void Consume(IConsumeContext<VoteAdded> context)
        {
            // todo: handle stuff

            _logger.Information("Added vote by {UserId} to venue {VenueId}.",
                context.Message.UserId, context.Message.CandidateReference);
        }
    }
}
