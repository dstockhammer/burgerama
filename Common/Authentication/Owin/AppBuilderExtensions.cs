using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using Burgerama.Common.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace Burgerama.Common.Authentication.Owin
{
    public static class AppBuilderExtensions
    {
        public static void UseAuth0(this IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            var config = ConfigurationManager.GetSection("burgerama/auth0") as Auth0Configuration;
            if (config == null)
                return;

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { config.Audience },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(config.Issuer, TextEncodings.Base64Url.Decode(config.Secret))
                }
            });
        }
    }
}
