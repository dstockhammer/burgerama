using Burgerama.Messaging.Commands.Voting;
using Burgerama.Shared.Candidates.Services.Contracts;
using MassTransit;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class CandidateHandler : Consumes<CreateCandidate>.All,
                                           Consumes<OpenCandidate>.All,
                                           Consumes<CloseCandidate>.All
    {
        private readonly ICandidateService _candidateService;

        public CandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public void Consume(CreateCandidate message)
        {
            _candidateService.CreateCandidate(message.ContextKey, message.Reference, message.OpeningDate, message.ClosingDate);
        }

        public void Consume(OpenCandidate message)
        {
            _candidateService.OpenCandidate(message.ContextKey, message.Reference, message.OpeningDate);
        }

        public void Consume(CloseCandidate message)
        {
            _candidateService.CloseCandidate(message.ContextKey, message.Reference, message.ClosingDate);
        }
    }
}
