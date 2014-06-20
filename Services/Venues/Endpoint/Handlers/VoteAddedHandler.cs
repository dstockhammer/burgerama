using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Venues.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class VoteAddedHandler : Consumes<VoteAdded>.Selected
    {
        private const string VenueContextKey = "venues";

        private readonly ILogger _logger;
        private readonly IVenueRepository _venueRepository;

        public VoteAddedHandler(ILogger logger, IVenueRepository venueRepository)
        {
            _logger = logger;
            _venueRepository = venueRepository;
        }

        public bool Accept(VoteAdded message)
        {
            return message.ContextKey == VenueContextKey;
        }

        public void Consume(VoteAdded message)
        {
            var venue = _venueRepository.Get(message.Reference);
            if (venue == null)
            {
                _logger.Error("Tried to update votes for unknown venue {Reference}.",
                    message.Reference);

                return;
            }

            venue.TotalVotes++;
            _venueRepository.SaveOrUpdate(venue);

            _logger.Information("Votes for venue {Reference} updated to {TotalVotes}.",
                message.Reference, venue.TotalVotes);
        }
    }
}
