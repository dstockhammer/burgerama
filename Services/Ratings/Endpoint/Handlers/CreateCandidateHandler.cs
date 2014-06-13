using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Services.Ratings.Domain.Contracts;
using Burgerama.Services.Ratings.Endpoint.Services;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class CreateCandidateHandler : Consumes<CreateCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly CandidateService _candidateService;
        private readonly ICandidateRepository _candidateRepository;

        public CreateCandidateHandler(ILogger logger, CandidateService candidateService, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _candidateService = candidateService;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<CreateCandidate> context)
        {
            var candidate = _candidateRepository.Get(context.Message.ContextKey, context.Message.Reference);
            if (candidate != null)
            {
                _logger.Error("Tried to create candidate {Reference} in context {ContextKey}, but it already exists.",
                    context.Message.Reference, context.Message.ContextKey);

                return;
            }

            _candidateService.CreateCandidate(context.Message.ContextKey, context.Message.Reference);
        }
    }
}
