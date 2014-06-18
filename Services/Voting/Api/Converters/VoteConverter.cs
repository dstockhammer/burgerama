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
                CreatedBy = vote.CreatedBy,
                CreatedOn = vote.CreatedOn
            };
        }

        public static Vote ToDomain(this VoteModel vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);

            return new Vote(vote.CreatedOn, vote.CreatedBy);
        }
    }
}