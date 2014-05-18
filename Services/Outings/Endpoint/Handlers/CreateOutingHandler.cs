using Burgerama.Messaging.Commands.Outings;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Outings.Endpoint.Handlers
{
    public sealed class CreateOutingHandler : Consumes<CreateOuting>.Context
    {
        private readonly ILogger _logger;
        private readonly IOutingRepository _outingRepository;

        public CreateOutingHandler(ILogger logger, IOutingRepository outingRepository)
        {
            _logger = logger;
            _outingRepository = outingRepository;
        }

        public void Consume(IConsumeContext<CreateOuting> context)
        {
            var outing = new Outing(context.Message.Date, context.Message.VenueId);
            _outingRepository.SaveOrUpdate(outing);

            _logger.Information("Created outing {@Outing} with Id \"{Id}\".", new { context.Message.VenueId, context.Message.Date }, outing.Id);
        }
    }
}
