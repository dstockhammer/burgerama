using Burgerama.Messaging.Commands.Outings;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using MassTransit;

namespace Burgerama.Services.Outings.Endpoint.Handlers
{
    public sealed class CreateOutingHandler : Consumes<CreateOuting>.Context
    {
        private readonly IOutingRepository _outingRepository;

        public CreateOutingHandler(IOutingRepository outingRepository)
        {
            _outingRepository = outingRepository;
        }

        public void Consume(IConsumeContext<CreateOuting> context)
        {
            var outing = new Outing(context.Message.Date, context.Message.VenueId);
            _outingRepository.SaveOrUpdate(outing);
        }
    }
}
