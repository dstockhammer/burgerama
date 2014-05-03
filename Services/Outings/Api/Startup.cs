﻿using System;
using System.Diagnostics.Contracts;
using Burgerama.Common.Authentication.Owin;
using Microsoft.Owin.Cors;
using Owin;

namespace Burgerama.Services.Outings.Api
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