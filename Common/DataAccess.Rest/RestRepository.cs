using Burgerama.Common.Configuration;
using RestSharp;
using System;
using System.Configuration;

namespace Burgerama.Common.DataAccess.Rest
{
    public abstract class RestRepository
    {
        private static ApiConfiguration ApiConfig
        {
            get { return (ApiConfiguration)ConfigurationManager.GetSection("burgerama/api"); }
        }

        private readonly Lazy<RestClient> _client;

        protected RestClient Client
        {
            get { return _client.Value; }
        }
        protected RestRepository()
        {
            _client = new Lazy<RestClient>(() => new RestClient(ApiConfig.RouteAddress));
        }
    }
}
