using Burgerama.Messaging.Events.Ratings;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class RatingUpdatedHandler : Consumes<RatingUpdated>.Context
    {
        private readonly ILogger _logger;

        public RatingUpdatedHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void Consume(IConsumeContext<RatingUpdated> context)
        {
            // todo: handle stuff

            _logger.Information("Rating for venue \"{VenueId}\" updated to {NewTotal}.",
                new { context.Message.VenueId, context.Message.NewTotal });
        }
    }
}
