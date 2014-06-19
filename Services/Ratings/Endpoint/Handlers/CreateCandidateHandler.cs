using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Shared.Candidates.Services.Contracts;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class CreateCandidateHandler : Consumes<CreateCandidate>.Context
    {
        private readonly ICandidateService _candidateService;

        public CreateCandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public void Consume(IConsumeContext<CreateCandidate> context)
        {
            _candidateService.CreateCandidate(context.Message.ContextKey, context.Message.Reference, context.Message.OpeningDate, context.Message.ClosingDate);
        }
    }
}
