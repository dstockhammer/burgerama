using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.Events.Candidates;

namespace Burgerama.Shared.Candidates.Domain
{
    public abstract class Candidate<T> where T : class
    {
        protected readonly HashSet<T> _items;

        public string ContextKey { get; private set; }

        public Guid Reference { get; private set; }

        public DateTime? OpeningDate { get; private set; }

        public DateTime? ClosingDate { get; private set; }

        public IEnumerable<T> Items
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
                return _items;
            }
        }

        protected Candidate(string contextKey, Guid reference)
            : this(contextKey, reference, Enumerable.Empty<T>())
        {
        }

        protected Candidate(string contextKey, Guid reference, IEnumerable<T> items, DateTime? openingDate = null, DateTime? closingDate = null)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(items != null);

            ContextKey = contextKey;
            Reference = reference;
            OpeningDate = openingDate;
            ClosingDate = closingDate;

            _items = new HashSet<T>(items);
        }

        /// <summary>
        /// Adds an item to the candidate.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public abstract IEnumerable<IEvent> AddItem(T item);

        /// <summary>
        /// Set the candidate to open on a specific date.
        /// </summary>
        /// <param name="date">The opening date.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> OpenOn(DateTime date)
        {
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            // don't overwrite the date
            if (OpeningDate.HasValue)
                return Enumerable.Empty<IEvent>();

            OpeningDate = date;

            return new IEvent[]
            {
                new CandidateOpened { ContextKey = ContextKey, Reference = Reference, OpeningDate = date }
            };
        }

        /// <summary>
        /// Set the candidate to close on a specific date.
        /// </summary>
        /// <param name="date">The close date.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of domain events that result from this command.</returns>
        public IEnumerable<IEvent> CloseOn(DateTime date)
        {
            Contract.Ensures(Contract.Result<IEnumerable<IEvent>>() != null);

            // don't overwrite the date
            if (ClosingDate.HasValue)
                return Enumerable.Empty<IEvent>();

            ClosingDate = date;

            return new IEvent[]
            {
                new CandidateClosed { ContextKey = ContextKey, Reference = Reference, ClosingDate = date }
            };
        }

        /// <summary>
        /// Returns whether the candidate allows adding items on a specific date.
        /// </summary>
        public bool IsActiveOn(DateTime date)
        {
            // don't allow rating if there is not opening date
            if (OpeningDate.HasValue == false)
                return false;

            // don't allow rating if candidate has not yet been opened
            if (OpeningDate.Value >= date)
                return false;

            // don't allow rating if candidate has been closed
            if (ClosingDate.HasValue && ClosingDate.Value <= date)
                return false;

            return true;
        }
    }
}
