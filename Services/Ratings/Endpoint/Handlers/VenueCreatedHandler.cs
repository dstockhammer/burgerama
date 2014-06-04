using Burgerama.Messaging.Events.Venues;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class VenueCreatedHandler : Consumes<VenueCreated>.Context
    {
        private readonly ILogger _logger;
        private readonly ICandidateRepository _candidateRepository;

        public VenueCreatedHandler(ILogger logger, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<VenueCreated> context)
        {
            var venue = new Candidate(context.Message.VenueId, context.Message.Title);
            _candidateRepository.SaveOrUpdate(venue);

            _logger.Information("Venue \"{VenueId}\" created.",
                new { context.Message.VenueId, context.Message.Title });
        }
    }
}
