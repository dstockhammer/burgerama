using Burgerama.Messaging.Events.Campaigns;
using MassTransit;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class VoteAddedHandler : Consumes<VoteAdded>.Context
    {
        public void Consume(IConsumeContext<VoteAdded> context)
        {
            // todo: handle stuff
        }
    }
}
