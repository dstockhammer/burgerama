using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Ratings;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Contracts;
using Serilog;

namespace Burgerama.Services.Ratings.Endpoint.Services
{
    public sealed class CandidateService
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IContextRepository _contextRepository;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ILogger logger, IEventDispatcher eventDispatcher, IContextRepository contextRepository, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _contextRepository = contextRepository;
            _candidateRepository = candidateRepository;
        }

        public Candidate CreateCandidate(string contextKey, Guid reference, DateTime? openingDate = null, DateTime? closingDate = null)
        {
            var context = _contextRepository.Get(contextKey);
            if (context == null)
            {
                _logger.Error("Tried to create candidate {Reference} in context {ContextKey}, but the context doesn't exist.",
                    reference, contextKey);

                return null;
            }

            if (context.AddCandidate(reference) == false)
            {
                _logger.Fatal("DATA INCONSISTENCY: The candidate {Reference} already exists in context {ContextKey}, but there is no actual data for the candidate.",
                    reference, contextKey);

                return null;
            }

            var candidate = new Candidate(contextKey, reference);
            var events = new List<IEvent>
            {
                new CandidateCreated { ContextKey = contextKey, Reference = reference }
            };

            if (openingDate.HasValue)
                events.AddRange(candidate.OpenOn(openingDate.Value));

            if (closingDate.HasValue)
                events.AddRange(candidate.CloseOn(closingDate.Value));

            var potentialCandidate = _candidateRepository.GetPotential(contextKey, reference);
            if (potentialCandidate != null)
            {
                foreach (var rating in potentialCandidate.Ratings)
                {
                    events.AddRange(candidate.AddRating(rating));
                }

                _logger.Information("Validated potential candidate {Reference} in context {ContextKey} and transferred {RatingCount} of {PotentialRatingCount} existing ratings.",
                    reference, contextKey, candidate.Ratings.Count(), potentialCandidate.Ratings.Count());

                _candidateRepository.Delete(potentialCandidate);
            }

            _contextRepository.SaveOrUpdate(context);
            _candidateRepository.SaveOrUpdate(candidate);

            _eventDispatcher.Publish((IEnumerable<IEvent>)events);

            _logger.Information("Created candidate {Reference} in context {ContextKey}.",
                reference, contextKey);

            return candidate;
        }
    }
}
