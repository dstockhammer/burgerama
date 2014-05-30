﻿
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
{
    internal static class VoteConverter
    {
        public static VoteModel ToModel(this Vote vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);
            Contract.Ensures(Contract.Result<VoteModel>() != null);

            return new VoteModel
            {
                Id = vote.Id.ToString(),
                CreatedBy = vote.CreatedBy,
                CreatedOn = vote.CreatedOn
            };
        }

        public static Vote ToDomain(this VoteModel vote)
        {
            Contract.Requires<ArgumentNullException>(vote != null);
            Contract.Ensures(Contract.Result<Vote>() != null);

            var id = Guid.Parse(vote.Id);
            return new Vote(id, vote.CreatedOn, vote.CreatedBy);
        }
    }
}
