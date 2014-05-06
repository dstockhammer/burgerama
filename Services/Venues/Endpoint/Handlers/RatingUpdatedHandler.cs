using Burgerama.Messaging.Events.Ratings;
using MassTransit;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class RatingUpdatedHandler : Consumes<RatingUpdated>.Context
    {
        public void Consume(IConsumeContext<RatingUpdated> context)
        {
            // todo: handle stuff
        }
    }
}
