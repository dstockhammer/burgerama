using Burgerama.Messaging.Events.Ratings;
using NServiceBus;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class RatingUpdatedHandler : IHandleMessages<RatingUpdated>
    {
        public void Handle(RatingUpdated message)
        {
            // todo: handle stuff
        }
    }
}
