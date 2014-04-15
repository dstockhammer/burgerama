using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Venues.Api;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Burgerama.Services.Venues.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            AuthConfig.Configure(app);
        }
    }
}
