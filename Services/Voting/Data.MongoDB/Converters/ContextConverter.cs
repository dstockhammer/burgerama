using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
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
                AllowToVoteForUnknownCandidates = context.AllowToVoteForUnknownCandidates,
                Candidates = context.Candidates
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            if (context == null)
                return null;

            return new Context(context.ContextKey, context.AllowToVoteForUnknownCandidates, context.Candidates);
        }
    }
}
