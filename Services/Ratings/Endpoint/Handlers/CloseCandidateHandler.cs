using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Shared.Candidates.Services.Contracts;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class CloseCandidateHandler : Consumes<CloseCandidate>.Context
    {
        private readonly ICandidateService _candidateService;

        public CloseCandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public void Consume(IConsumeContext<CloseCandidate> context)
        {
            _candidateService.CloseCandidate(context.Message.ContextKey, context.Message.Reference, context.Message.ClosingDate);
        }
    }
}
