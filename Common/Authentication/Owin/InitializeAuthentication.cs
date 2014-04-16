using System;
using System.Diagnostics.Contracts;
using Owin;

namespace Burgerama.Common.Authentication.Owin
{
    public sealed class InitializeAuthentication
    {
        public void Configuration(IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            OwinAuthentication.Configure(app);
        }
    }
}
