using System.Collections.Generic;
using System.Linq;
using Burgerama.Services.OutingScheduler.Data.Rest.Converters;
using Burgerama.Services.OutingScheduler.Data.Rest.Models;
using Burgerama.Services.OutingScheduler.Domain;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using RestSharp;

namespace Burgerama.Services.OutingScheduler.Data.Rest
{
    public sealed class VenueRepository : IVenueRepository
    {
        public IEnumerable<Venue> GetAll()
        {
            var client = new RestClient("http://api.dev.burgerama.co.uk");
            var request = new RestRequest("venues", Method.GET);
            var response = client.Execute<List<VenueModel>>(request);

            return response.Data.Select(o => o.ToDomain());
        }
    }
}
