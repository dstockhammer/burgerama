using System;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Outings;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using NServiceBus;

namespace Burgerama.Services.Outings.Endpoint.Handlers
{
    public sealed class CreateOutingHandler : IHandleMessages<CreateOuting>
    {
        private readonly IBus _bus;
        private readonly IOutingRepository _outingRepository;

        public CreateOutingHandler(IBus bus, IOutingRepository outingRepository)
        {
            _bus = bus;
            _outingRepository = outingRepository;
        }

        public void Handle(CreateOuting message)
        {
            Console.WriteLine("Creating outing for: " + message.VenueId);

            var outing = new Outing(message.Date, message.VenueId);
            _outingRepository.SaveOrUpdate(outing);

            _bus.Return(StatusCode.Ok);
        }
    }
}
