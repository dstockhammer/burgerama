using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Api.Converters
{
    internal static class ContextConverter
    {
        public static ContextModel ToModel(this Context context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            return new ContextModel
            {
                ContextKey = context.ContextKey,
                Candidates = context.Candidates
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            return new Context(context.ContextKey, context.Candidates);
        }
    }
}