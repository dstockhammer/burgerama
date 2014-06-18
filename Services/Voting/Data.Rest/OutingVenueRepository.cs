using Burgerama.Common.DataAccess.Rest;
using Burgerama.Services.Voting.Data.Rest.Models;
using RestSharp;
using System;

namespace Burgerama.Services.Voting.Data.Rest
{
    internal sealed class OutingVenueRepository : RestRepository
    {
        public OutingVenueModel Get(Guid venueId)
        {
            var request = new RestRequest("venues/{id}", Method.GET);
            request.AddUrlSegment("id", venueId.ToString());
            var response = Client.Execute<OutingVenueModel>(request);

            return response.Data;
        }
    }
}
