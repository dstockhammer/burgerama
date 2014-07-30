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
                GracefullyHandleUnknownCandidates = context.GracefullyHandleUnknownCandidates
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            if (context == null)
                return null;

            return new Context(context.ContextKey, context.GracefullyHandleUnknownCandidates);
        }
    }
}
