
using Burgerama.Services.Voting.Data.MongoDB.Models;
using Burgerama.Services.Voting.Domain;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Burgerama.Services.Voting.Data.MongoDB.Converters
{
    internal static class ContextConverter
    {
        public static ContextModel ToModel(this Context context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Contract.Result<ContextModel>() != null);

            return new ContextModel
            {
                Id = context.Id.ToString(),
                Campaigns = context.Campaigns.Select(c => CampaignConverter.ToModel(c)).ToList()
            };
        }

        public static Context ToDomain(this ContextModel context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Contract.Result<Context>() != null);

            var id = Guid.Parse(context.Id);
            return new Context(id, context.Key, context.Campaigns.Select(c => c.ToDomain()));
        }
    }
}
