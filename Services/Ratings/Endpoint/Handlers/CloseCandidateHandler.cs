using System;
using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Domain.Contracts;
using Burgerama.Services.Ratings.Endpoint.Services;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class CloseCandidateHandler : Consumes<CloseCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly CandidateService _candidateService;
        private readonly ICandidateRepository _candidateRepository;

        public CloseCandidateHandler(ILogger logger, IEventDispatcher eventDispatcher, CandidateService candidateService, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _candidateService = candidateService;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<CloseCandidate> context)
        {
            var candidate = _candidateRepository.Get(context.Message.ContextKey,context.Message.Reference);
            if (candidate == null)
            {
                _logger.Warning("Tried to close candidate {Reference} in context {ContextKey}, but it doesn't exist. Creating candidate...",
                    context.Message.Reference, context.Message.ContextKey);

                // since the candidate is about to be closed, it is assumed that it was open before. it is therefore opened immediately.
                var openingDate = DateTime.Now;
                if (openingDate > context.Message.ClosingDate)
                    openingDate = context.Message.ClosingDate;

                candidate = _candidateService.CreateCandidate(context.Message.ContextKey, context.Message.Reference, openingDate);
            }

            var events = candidate.CloseOn(context.Message.ClosingDate);

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish(events);

            _logger.Information("Closed candidate {Reference} in context {ContextKey} on {ClosingDate}.",
                context.Message.Reference, context.Message.ContextKey, context.Message.ClosingDate);
        }
    }
}
