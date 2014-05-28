﻿using Burgerama.Common.DataAccess.Rest;
using Burgerama.Services.Voting.Data.Converters;
using Burgerama.Services.Voting.Data.Models;
using Burgerama.Services.Voting.Domain;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Burgerama.Services.Voting.Data
{
    public sealed class VenueRestRepository : RestRepository
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
