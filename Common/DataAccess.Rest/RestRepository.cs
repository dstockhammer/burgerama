using Burgerama.Common.Configuration;
using RestSharp;
using System;

namespace Burgerama.Common.DataAccess.Rest
{
    public abstract class RestRepository
    {
        private static readonly Lazy<ApiConfiguration> ApiConfig = new Lazy<ApiConfiguration>(ApiConfiguration.Load);
        private readonly Lazy<RestClient> _client;

        protected RestClient Client
        {
            get { return _client.Value; }
        }

        protected RestRepository()
        {
            _client = new Lazy<RestClient>(GetClient);
        }

        protected abstract string GetTargetServiceKey();

        private RestClient GetClient()
        {
            var url = ApiConfig.Value.RouteAddress;
            var targetServiceKey = GetTargetServiceKey();
            if (targetServiceKey != null)
                url = url.Replace("{service}", targetServiceKey);

            return new RestClient(url);
        }
    }
}
