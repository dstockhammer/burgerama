using Burgerama.Services.Ratings.Data.Models;
using Burgerama.Services.Ratings.Domain;

namespace Burgerama.Services.Ratings.Data.Converters
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
                AllowToRateUnknownCandidates = context.AllowToRateUnknownCandidates,
                Candidates = context.Candidates
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            if (context == null)
                return null;

            return new Context(context.ContextKey, context.AllowToRateUnknownCandidates, context.Candidates);
        }
    }
}
