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
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ILogger logger, IEventDispatcher eventDispatcher, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _candidateRepository = candidateRepository;
        }

        public Candidate CreateCandidate(string contextKey, Guid reference, DateTime? openingDate = null, DateTime? closingDate = null)
        {
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
                _logger.Information("Validated potential candidate {Reference} in context {ContextKey} and transferred {RatingCount} existing ratings.",
                    reference, contextKey, potentialCandidate.Ratings.Count());

                foreach (var rating in potentialCandidate.Ratings)
                {
                    events.AddRange(candidate.AddRating(rating));
                }

                _candidateRepository.Delete(potentialCandidate);
            }

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish((IEnumerable<IEvent>)events);

            _logger.Information("Created candidate {Reference} in context {ContextKey}.",
                reference, contextKey);

            return candidate;
        }
    }
}
