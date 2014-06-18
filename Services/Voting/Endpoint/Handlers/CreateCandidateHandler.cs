using Burgerama.Messaging.Commands.Voting;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Voting;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Contracts;
using MassTransit;
using Serilog;
using System;
using System.Linq;

namespace Burgerama.Services.Voting.Endpoint.Handlers
{
    public sealed class CreateCandidateHandler : Consumes<CreateCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IContextRepository _contextRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CreateCandidateHandler(
            ILogger logger, 
            ICandidateRepository candidateRepository, 
            IContextRepository contextRepository, 
            IEventDispatcher eventDispatcher)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _contextRepository = contextRepository;
            _eventDispatcher = eventDispatcher;
        }

        public void Consume(IConsumeContext<CreateCandidate> context)
        {
            var reference = Guid.Parse(context.Message.Reference);
            var contextKey = context.Message.ContextKey;
            var candidate = _candidateRepository.Get(reference, contextKey);

            if (candidate == null)
            {
                candidate = new Candidate(reference);
                _candidateRepository.SaveOrUpdate(candidate, contextKey);

                var votingContext = _contextRepository.Get(contextKey);
                votingContext.AddCandidate(reference);
                _contextRepository.SaveOrUpdate(votingContext);
                
                _logger.Information(
                    "Candidate with \"{Reference}\" reference is created under \"{ContextKey}\" context.",
                    new { context.Message.Reference, contextKey });
            }

            candidate.Vote(context.Message.UserId, context.Message.VotedOn);
            _candidateRepository.SaveOrUpdate(candidate, contextKey);

            _eventDispatcher.Publish(new VoteAdded
            {
                CandidateReference = candidate.Reference,
                ContextKey = contextKey,
                UserId = context.Message.UserId,
                TotalOfVotes = candidate.Votes.Count()
            });
        }
    }
}
