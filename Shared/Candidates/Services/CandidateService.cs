using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Candidates;
using Burgerama.Shared.Candidates.Domain;
using Burgerama.Shared.Candidates.Domain.Contracts;
using Burgerama.Shared.Candidates.Services.Contracts;
using Serilog;

namespace Burgerama.Shared.Candidates.Services
{
    public sealed class CandidateService<T> : ICandidateService
        where T : class
    {
        private readonly ILogger _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IContextRepository _contextRepository;
        private readonly ICandidateFactory _candidateFactory;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(
            ILogger logger,
            IEventDispatcher eventDispatcher,
            IContextRepository contextRepository,
            ICandidateFactory candidateFactory,
            ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
            _contextRepository = contextRepository;
            _candidateFactory = candidateFactory;
            _candidateRepository = candidateRepository;
        }

        public void CreateCandidate(string contextKey, Guid reference, DateTime? openingDate = null, DateTime? closingDate = null)
        {
            var candidate = _candidateRepository.Get<Candidate<T>, T>(contextKey, reference);
            if (candidate != null)
            {
                _logger.Error("Tried to create candidate {Reference} in context {ContextKey}, but it already exists.",
                    reference, contextKey);

                return;
            }

            CreateInternal(contextKey, reference, openingDate, closingDate);
        }

        public void CloseCandidate(string contextKey, Guid reference, DateTime closingDate)
        {
            var candidate = _candidateRepository.Get<Candidate<T>, T>(contextKey, reference);
            if (candidate == null)
            {
                _logger.Warning("Tried to close candidate {Reference} in context {ContextKey}, but it doesn't exist. Creating candidate...",
                    reference, contextKey);

                // since the candidate is about to be closed, it is assumed that it was open before. it is therefore opened immediately.
                var openingDate = DateTime.Now;
                if (openingDate > closingDate)
                    openingDate = closingDate;

                candidate = CreateInternal(contextKey, reference, openingDate);

                if (candidate == null)
                    return;
            }

            var events = candidate.CloseOn(closingDate);

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish(events);

            _logger.Information("Closed candidate {Reference} in context {ContextKey} on {ClosingDate}.",
                reference, contextKey, closingDate);
        }

        public void OpenCandidate(string contextKey, Guid reference, DateTime openingDate)
        {
            var candidate = _candidateRepository.Get<Candidate<T>, T>(contextKey, reference);
            if (candidate == null)
            {
                _logger.Warning("Tried to open candidate {Reference} in context {ContextKey}, but it doesn't exist. Creating candidate...",
                    reference, contextKey);

                candidate = CreateInternal(contextKey, reference);

                if (candidate == null)
                    return;
            }

            var events = candidate.OpenOn(openingDate);

            _candidateRepository.SaveOrUpdate(candidate);
            _eventDispatcher.Publish(events);

            _logger.Information("Opened candidate {Reference} in context {ContextKey} on {OpeningDate}.",
                reference, contextKey, openingDate);
        }

        private Candidate<T> CreateInternal(string contextKey, Guid reference, DateTime? openingDate = null, DateTime? closingDate = null)
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

            var candidate = _candidateFactory.Create<T>(contextKey, reference);
            var events = new List<IEvent>
            {
                new CandidateCreated { ContextKey = contextKey, Reference = reference }
            };

            if (openingDate.HasValue)
                events.AddRange(candidate.OpenOn(openingDate.Value));

            if (closingDate.HasValue)
                events.AddRange(candidate.CloseOn(closingDate.Value));

            var potentialCandidate = _candidateRepository.GetPotential<PotentialCandidate<T>, T>(contextKey, reference);
            if (potentialCandidate != null)
            {
                foreach (var item in potentialCandidate.Items)
                {
                    events.AddRange(candidate.AddItem(item));
                }

                _logger.Information("Validated potential candidate {Reference} in context {ContextKey} and transferred {ItemCount} of {PotentialItemCount} existing items.",
                    reference, contextKey, candidate.Items.Count(), potentialCandidate.Items.Count());

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
