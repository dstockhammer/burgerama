using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class RatingUpdatedHandler : Consumes<RatingUpdated>.Context
    {
        private const string VenueContextKey = "venues";

        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;

        public RatingUpdatedHandler(ILogger logger, IVenueRepository venueRepository)
        {
            _logger = logger;
            _venueRepository = venueRepository;
        }

        public void Consume(IConsumeContext<RatingUpdated> context)
        {
            // ignore events that don't belong to the venue context
            if (context.Message.ContextKey != VenueContextKey)
                return;

            var venue = _venueRepository.Get(context.Message.Reference);
            if (venue == null)
            {
                _logger.Error("Tried to update rating for unknown venue \"{Reference}\".",
                    new { context.Message.Reference });

                return;
            }

            venue.TotalRating = context.Message.NewTotal;
            _venueRepository.SaveOrUpdate(venue);

            _logger.Information("Rating for venue \"{Reference}\" updated to {NewTotal}.",
                new { context.Message.Reference, context.Message.NewTotal });
        }
    }
}
