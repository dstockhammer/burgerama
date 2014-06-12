using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class CloseCandidateHandler : Consumes<CloseCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICandidateRepository _candidateRepository;

        public CloseCandidateHandler(ILogger logger, IEventDispatcher eventDispatcher, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<CloseCandidate> context)
        {
            var candidate = _candidateRepository.Get(context.Message.Reference, context.Message.ContextKey);
            if (candidate == null)
            {
                _logger.Error("Tried to close candidate \"{Reference}\" in context \"{ContextKey}\", but it doesn't exist.",
                    new { context.Message.Reference, context.Message.ContextKey });

                return;
            }

            var events = candidate.CloseOn(context.Message.ClosingDate);

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish(events);
            _logger.Information("Closed candidate \"{Reference}\" in context \"{ContextKey}\" on {ClosingDate}.",
                new { context.Message.Reference, context.Message.ContextKey, context.Message.ClosingDate });
        }
    }
}
