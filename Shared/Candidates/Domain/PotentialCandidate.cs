using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Shared.Candidates.Domain
{
    public abstract class PotentialCandidate<T> where T : class
    {
        protected readonly HashSet<T> _items;

        public string ContextKey { get; private set; }

        public Guid Reference { get; private set; }

        public IEnumerable<T> Items
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
                return _items;
            }
        }

        protected PotentialCandidate(string contextKey, Guid reference)
            : this(contextKey, reference, Enumerable.Empty<T>())
        {
        }

        protected PotentialCandidate(string contextKey, Guid reference, IEnumerable<T> items)
        {
            Contract.Requires<ArgumentNullException>(contextKey != null);
            Contract.Requires<ArgumentNullException>(items != null);

            ContextKey = contextKey;
            Reference = reference;

            _items = new HashSet<T>(items);
        }

        /// <summary>
        /// Adds a rating to the potential candidate.
        /// </summary>
        public virtual void AddItem(T item)
        {
            Contract.Requires<ArgumentNullException>(item != null);

            // it doesn't matter if the item was added successfully or whether the candidate
            // is active at all. just add everything, it will be sorted out later.
            _items.Add(item);
        }
    }
}
