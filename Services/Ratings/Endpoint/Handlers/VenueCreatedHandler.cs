﻿using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
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
