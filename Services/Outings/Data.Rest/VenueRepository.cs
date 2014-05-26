using System;
using System.Collections.Generic;
using System.Linq;
using Burgerama.Services.Outings.Data.Rest.Converters;
using Burgerama.Services.Outings.Data.Rest.Models;
using Burgerama.Services.Outings.Domain;
using Burgerama.Services.Outings.Domain.Contracts;
using RestSharp;

namespace Burgerama.Services.Outings.Data.Rest
{
    public sealed class VenueRepository : RestRepository, IVenueRepository
    {
        public Venue Get(Guid venueId)
        {
            var request = new RestRequest("venues/{id}", Method.GET);
            request.AddUrlSegment("id", venueId.ToString());
            var response = Client.Execute<VenueModel>(request);

            return response.Data.ToDomain();
        }

        public IEnumerable<Venue> GetAll()
        {
            var request = new RestRequest("venues", Method.GET);
            var response = Client.Execute<List<VenueModel>>(request);

            return response.Data.Select(v => v.ToDomain());
        }
    }
}
