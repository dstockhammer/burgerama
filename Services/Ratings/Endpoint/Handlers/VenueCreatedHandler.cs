using System;
using Burgerama.Messaging.Events.Venues;
using NServiceBus;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class VenueCreatedHandler : IHandleMessages<VenueCreated>
    {
        public void Handle(VenueCreated message)
        {
            // todo: handle stuff
            Console.WriteLine("venue created: {0}", message.Title);
        }
    }
}
