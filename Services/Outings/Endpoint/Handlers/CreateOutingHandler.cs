using Burgerama.Messaging.Commands.Outings;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Outings;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Outings.Endpoint.Handlers
{
    public sealed class CreateOutingHandler : Consumes<CreateOuting>.Context
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IOutingRepository _outingRepository;

        public CreateOutingHandler(ILogger logger, IEventDispatcher eventDispatcher, IOutingRepository outingRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _outingRepository = outingRepository;
        }

        public void Consume(IConsumeContext<CreateOuting> context)
        {
            var outing = new Outing(context.Message.Date, context.Message.VenueId);
            _outingRepository.SaveOrUpdate(outing);

            _eventDispatcher.Publish(new OutingCreated
            {
                OutingId = outing.Id,
                VenueId = outing.VenueId,
                DateTime = outing.Date
            });

            _logger.Information("Created outing {@Outing} with Id {Id}.",
                new { context.Message.VenueId, context.Message.Date }, outing.Id);
        }
    }
}
