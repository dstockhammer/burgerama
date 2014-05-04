using Burgerama.Messaging.Events.Outings;
using NServiceBus;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class OutingCreatedHandler : IHandleMessages<OutingCreated>
    {
        public void Handle(OutingCreated message)
        {
            // todo: handle stuff
        }
    }
}
