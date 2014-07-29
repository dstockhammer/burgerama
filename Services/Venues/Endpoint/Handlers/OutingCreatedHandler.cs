using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Venues.Domain;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class OutingCreatedHandler : Consumes<OutingCreated>.All
    {
        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;

        public OutingCreatedHandler(ILogger logger, IVenueRepository venueRepository)
        {
            _logger = logger;
            _venueRepository = venueRepository;
        }

        public void Consume(OutingCreated message)
        {
            var venue = _venueRepository.Get(message.VenueId);
            if (venue == null)
            {
                _logger.Error("Tried to add outing {OutingId} to unknown venue {VenueId}.",
                    message.OutingId, message.VenueId);

                return;
            }

            var outing = new Outing(message.OutingId, message.DateTime);
            if (venue.AddOuting(outing) == false)
            {
                _logger.Error("Tried to add outing {OutingId} to venue {VenueId}, but the outing already existed.",
                    message.OutingId, message.VenueId);

                return;
            }

            _venueRepository.SaveOrUpdate(venue);

            _logger.Information("Outing {OutingId} added to {VenueId}.",
                message.OutingId, message.VenueId);
        }
    }
}
