using System;
using System.Linq;
using Burgerama.Shared.Candidates.Data.MongoDB.Models;
using Burgerama.Shared.Candidates.Domain;

namespace Burgerama.Shared.Candidates.Data.MongoDB.Converters
{
    internal static class ContextConverter
    {
        public static ContextModel ToModel(this Context context)
        {
            if (context == null)
                return null;

            return new ContextModel
            {
                ContextKey = context.ContextKey,
                GracefullyHandleUnknownCandidates = context.GracefullyHandleUnknownCandidates,
                Candidates = context.Candidates.Select(c => c.ToString())
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            if (context == null)
                return null;

            var candidates = context.Candidates.Select(Guid.Parse);
            return new Context(context.ContextKey, context.GracefullyHandleUnknownCandidates, candidates);
        }
    }
}
