
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
{
    internal static class ContextConverter
    {
        public static ContextModel ToModel(this Context context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Contract.Result<ContextModel>() != null);

            return new ContextModel
            {
                ContextKey = context.ContextKey,
                Candidates = context.Candidates.Select(c => c.ToString()).ToList()
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Contract.Result<Context>() != null);

            return new Context(context.ContextKey, context.Candidates.Select(Guid.Parse));
        }
    }
}
