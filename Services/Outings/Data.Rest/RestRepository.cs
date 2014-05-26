using System;
using RestSharp;

namespace Burgerama.Services.Outings.Data.Rest
{
    // todo: copied from outingscheduler... move this into a nuget package
    public abstract class RestRepository
    {
        private readonly Lazy<RestClient> _client;

        protected RestRepository()
        {
            // todo: get the api url from the config
            const string url = "http://api.dev.burgerama.co.uk";

            _client = new Lazy<RestClient>(() => new RestClient(url));
        }

        protected RestClient Client
        {
            get { return _client.Value; }
        }
    }
}
