using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Shared.Candidates.Services.Contracts;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class ContextHandler : Consumes<CreateContext>.All
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IContextService _contextService;

        public ContextHandler(IEventDispatcher eventDispatcher, IContextService contextService)
        {
            _eventDispatcher = eventDispatcher;
            _contextService = contextService;
        }

        public void Consume(CreateContext message)
        {
            var success = _contextService.CreateContext(message.ContextKey, message.GracefullyHandleUnknownCandidates);
            if (success == false)
                return;

            _eventDispatcher.Publish(new ContextCreated
            {
                ContextKey = message.ContextKey
            });
        }
    }
}
