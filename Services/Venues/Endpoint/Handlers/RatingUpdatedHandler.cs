using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class RatingUpdatedHandler : Consumes<RatingUpdated>.Selected
    {
        private const string VenueContextKey = "venues";

        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;

        public RatingUpdatedHandler(ILogger logger, IVenueRepository venueRepository)
        {
            _logger = logger;
            _venueRepository = venueRepository;
        }

        public bool Accept(RatingUpdated message)
        {
            return message.ContextKey == VenueContextKey;
        }

        public void Consume(RatingUpdated message)
        {
            var venue = _venueRepository.Get(message.Reference);
            if (venue == null)
            {
                _logger.Error("Tried to update rating for unknown venue {Reference}.",
                    message.Reference);

                return;
            }

            venue.TotalRating = message.NewTotal;
            _venueRepository.SaveOrUpdate(venue);

            _logger.Information("Rating for venue {Reference} updated to {TotalRating}.",
                message.Reference, venue.TotalRating);
        }
    }
}
