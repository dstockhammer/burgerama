using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Domain.Contracts;
using Burgerama.Services.Ratings.Endpoint.Services;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class OpenCandidateHandler : Consumes<OpenCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly CandidateService _candidateService;
        private readonly ICandidateRepository _candidateRepository;

        public OpenCandidateHandler(ILogger logger, IEventDispatcher eventDispatcher, CandidateService candidateService, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _candidateService = candidateService;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<OpenCandidate> context)
        {
            var candidate = _candidateRepository.Get(context.Message.ContextKey, context.Message.Reference);
            if (candidate == null)
            {
                _logger.Warning("Tried to open candidate {Reference} in context {ContextKey}, but it doesn't exist. Creating candidate...",
                    context.Message.Reference, context.Message.ContextKey);

                candidate = _candidateService.CreateCandidate(context.Message.ContextKey, context.Message.Reference);
            }

            var events = candidate.OpenOn(context.Message.OpeningDate);

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish(events);

            _logger.Information("Opened candidate {Reference} in context {ContextKey} on {OpeningDate}.",
                context.Message.Reference, context.Message.ContextKey, context.Message.OpeningDate);
        }
    }
}
