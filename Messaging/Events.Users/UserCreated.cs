using System;

namespace Burgerama.Messaging.Events.Users
{
    [Serializable]
    public sealed class UserCreated : IEvent
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
    }
}
