using Burgerama.Messaging.Commands.Voting;
using Burgerama.Services.Voting.Domain.Contracts;
using MassTransit;
using Serilog;
using System;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{

    public sealed class ExpiryCandidateHandler : Consumes<ExpireCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly ICandidateRepository _candidateRepository;

        public ExpiryCandidateHandler(
            ILogger logger, 
            ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<ExpireCandidate> context)
        {
            var reference = Guid.Parse(context.Message.Reference);
            var candidate = _candidateRepository.Get(context.Message.ContextKey, reference);

            if (candidate != null)
            {
                candidate.CloseOn(context.Message.ExpiryDate);
                _candidateRepository.SaveOrUpdate(candidate);

                _logger.Information("Expiration date {ExpiryDate} set on candidate with {Reference} reference under {ContextKey} context.",
                    context.Message.ExpiryDate, context.Message.Reference, context.Message.ContextKey);
            }
        }
    }
}
