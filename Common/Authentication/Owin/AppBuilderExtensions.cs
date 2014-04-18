using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;
using Burgerama.Common.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Burgerama.Common.Authentication.Owin
{
    public static class AppBuilderExtensions
    {
        public static void UseAuth0(this IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            var config = (ServiceConfiguration)ConfigurationManager.GetSection("burgerama.service");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { config.Auth0.Audience },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(config.Auth0.Issuer, TextEncodings.Base64Url.Decode(config.Auth0.Secret))
                },
                Provider = new OAuthBearerAuthenticationProvider
                {
                    OnValidateIdentity = context =>
                    {
                        context.Ticket.Identity.AddClaim(new Claim("foo", "bar"));
                        return Task.FromResult<object>(null);
                    }
                }
            });
        }
    }
}
