using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
{
    internal static class VoteConverter
    {
        public static VoteModel ToModel(this Vote vote)
        {
            if (vote == null)
                return null;

            return new VoteModel
            {
                CreatedOn = vote.CreatedOn,
                UserId = vote.UserId
            };
        }

        public static Vote ToDomain(this VoteModel vote)
        {
            if (vote == null)
                return null;

            return new Vote(vote.CreatedOn, vote.UserId);
        }
    }
}
