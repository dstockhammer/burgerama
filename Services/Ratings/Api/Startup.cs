
using Burgerama.Common.Authentication.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Ratings.Api
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            app.UseAuth0();
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}