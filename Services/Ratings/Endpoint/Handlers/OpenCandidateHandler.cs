using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Shared.Candidates.Services.Contracts;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class OpenCandidateHandler : Consumes<OpenCandidate>.Context
    {
        private readonly ICandidateService _candidateService;

        public OpenCandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public void Consume(IConsumeContext<OpenCandidate> context)
        {
            _candidateService.OpenCandidate(context.Message.ContextKey, context.Message.Reference, context.Message.OpeningDate);
        }
    }
}
