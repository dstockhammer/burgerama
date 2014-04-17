using System;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Burgerama.Common.Authentication.Owin
{
    internal static class OwinAuthentication
    {
        private const string Issuer = "https://burgerama.auth0.com/";
        private const string Audience = "xlaKo4Eqj5DbAJ44BmUGQhUF548TNc4Z";
        private const string Secret = "nope";

        public static void Configure(IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { Audience },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(Issuer, TextEncodings.Base64Url.Decode(Secret))
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
