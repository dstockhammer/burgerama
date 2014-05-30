using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
{
    internal static class CampaignConverter
    {
        public static CampaignModel ToModel(this Candidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
            Contract.Ensures(Contract.Result<CampaignModel>() != null);

            return new CampaignModel
            {
                Id = candidate.Id.ToString(),
                End = candidate.Expiry,
                Start = candidate.Start,
                Candidates = candidate.Candidates.Select(c => c.ToModel()).ToList()
            };
        }

        public static Candidate ToDomain(this CampaignModel campaign)
        {
            Contract.Requires<ArgumentNullException>(campaign != null);
            Contract.Ensures(Contract.Result<Candidate>() != null);

            var id = Guid.Parse(campaign.Id);
            return new Candidate(id, campaign.Candidates.Select(c => c.ToDomain()), campaign.Start, campaign.End);
        }
    }
}
