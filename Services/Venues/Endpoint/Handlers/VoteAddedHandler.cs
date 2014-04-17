using Burgerama.Messaging.Events.Voting;
using NServiceBus;

namespace Burgerama.Services.Venues.Endpoint.Handlers
{
    public sealed class VoteAddedHandler : IHandleMessages<VoteAdded>
    {
        public void Handle(VoteAdded message)
        {
            // todo: handle stuff
        }
    }
}
