using Burgerama.Services.Voting.Api.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Api.Converters
{
    internal static class VoteConverter
    {
        public static VoteModel ToModel(this Vote vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);

            return new VoteModel
            {
                CreatedOn = vote.CreatedOn,
                UserId = vote.UserId
            };
        }

        public static Vote ToDomain(this VoteModel vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);

            return new Vote(vote.CreatedOn, vote.UserId);
        }
    }
}