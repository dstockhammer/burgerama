using System.Collections.Generic;
using System.Linq;
using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using MassTransit;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Handlers
{
    public sealed class CreateCandidateHandler : Consumes<CreateCandidate>.Context
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICandidateRepository _candidateRepository;

        public CreateCandidateHandler(ILogger logger, IEventDispatcher eventDispatcher, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _candidateRepository = candidateRepository;
        }

        public void Consume(IConsumeContext<CreateCandidate> context)
        {
            var candidate = _candidateRepository.Get(context.Message.Reference, context.Message.ContextKey);
            if (candidate != null)
            {
                _logger.Error("Tried to create candidate \"{Reference}\" in context \"{ContextKey}\", but it already exists.",
                    new { context.Message.Reference, context.Message.ContextKey });

                return;
            }

            candidate = new Candidate(context.Message.ContextKey, context.Message.Reference, Enumerable.Empty<Rating>());

            var events = new List<IEvent>
            {
                new CandidateCreated { ContextKey = context.Message.ContextKey, Reference = context.Message.Reference }
            };

            if (context.Message.OpeningDate.HasValue)
                events.AddRange(candidate.OpenOn(context.Message.OpeningDate.Value));

            if (context.Message.ClosingDate.HasValue)
                events.AddRange(candidate.CloseOn(context.Message.ClosingDate.Value));
            
            var potentialCandidate = _candidateRepository.GetPotential(context.Message.Reference, context.Message.ContextKey);
            if (potentialCandidate != null)
            {
                _logger.Information("Validated potential candidate \"{Reference}\" in context \"{ContextKey}\" and transferred {RatingCount} existing ratings.",
                    new { context.Message.Reference, context.Message.ContextKey, RatingCount = potentialCandidate.Ratings.Count() });

                foreach (var rating in potentialCandidate.Ratings)
                {
                    events.AddRange(candidate.AddRating(rating));
                }

                _candidateRepository.Delete(potentialCandidate);
            }

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish((IEnumerable<IEvent>)events);
            _logger.Information("Created candidate \"{Reference}\" in context \"{ContextKey}\".",
                new { context.Message.Reference, context.Message.ContextKey });
        }
    }
}
