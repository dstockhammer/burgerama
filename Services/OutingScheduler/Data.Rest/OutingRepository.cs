using System.Collections.Generic;
using System.Linq;
using Burgerama.Services.OutingScheduler.Data.Rest.Converters;
using Burgerama.Services.OutingScheduler.Data.Rest.Models;
using Burgerama.Services.OutingScheduler.Domain;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using RestSharp;

namespace Burgerama.Services.OutingScheduler.Data.Rest
{
    public sealed class OutingRepository : RestRepository, IOutingRepository
    {
        public IEnumerable<Outing> GetAll()
        {
            var request = new RestRequest("outings", Method.GET);
            var response = Client.Execute<List<OutingModel>>(request);

            return response.Data.Select(o => o.ToDomain());
        }
    }
}
