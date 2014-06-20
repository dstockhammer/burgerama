using Burgerama.Messaging.Commands.Voting;
using Burgerama.Shared.Candidates.Services.Contracts;
using MassTransit;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class CandidateHandler : Consumes<CreateCandidate>.Context,
                                           Consumes<OpenCandidate>.Context,
                                           Consumes<CloseCandidate>.Context
    {
        private readonly ICandidateService _candidateService;

        public CandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public void Consume(IConsumeContext<CreateCandidate> context)
        {
            _candidateService.CreateCandidate(context.Message.ContextKey, context.Message.Reference, context.Message.OpeningDate, context.Message.ClosingDate);
        }

        public void Consume(IConsumeContext<OpenCandidate> context)
        {
            _candidateService.OpenCandidate(context.Message.ContextKey, context.Message.Reference, context.Message.OpeningDate);
        }

        public void Consume(IConsumeContext<CloseCandidate> context)
        {
            _candidateService.CloseCandidate(context.Message.ContextKey, context.Message.Reference, context.Message.ClosingDate);
        }
    }
}
