using Burgerama.Common.DataAccess;
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
        public IEnumerable<Venue> GetAll()
        {
            var request = new RestRequest("venues", Method.GET);
            var response = Client.Execute<List<VenueModel>>(request);

            return response.Data.Select(v => v.ToDomain());
        }
    }
}
