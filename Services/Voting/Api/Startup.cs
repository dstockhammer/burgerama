using System;
using System.Diagnostics.Contracts;
using Burgerama.Common.Authentication.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Serilog.Extras.MSOwin;

namespace Burgerama.Services.Voting.Api
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            app.UseAuth0();
            app.UseCors(CorsOptions.AllowAll);
            app.UseSerilogRequestContext();
        }
    }
}