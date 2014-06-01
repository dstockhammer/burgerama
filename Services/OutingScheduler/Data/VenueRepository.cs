using Burgerama.Common.DataAccess.Rest;
using Burgerama.Services.OutingScheduler.Data.Rest.Converters;
using Burgerama.Services.OutingScheduler.Data.Rest.Models;
using Burgerama.Services.OutingScheduler.Domain;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Burgerama.Services.OutingScheduler.Data.Rest
{
    public sealed class VenueRepository : RestRepository, IVenueRepository
    {
        protected override string GetTargetServiceKey()
        {
            return "venues";
        }

        public IEnumerable<Venue> GetAll()
        {
            var request = new RestRequest(Method.GET);
            var response = Client.Execute<List<VenueModel>>(request);

            return response.Data.Select(v => v.ToDomain());
        }
    }
}
