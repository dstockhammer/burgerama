using System;
using System.Diagnostics.Contracts;
using Burgerama.Services.Users.Api.Providers;
using Burgerama.Services.Users.Core;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Burgerama.Services.Users.Api
{
    public static class AuthConfig
    {
        public const string PublicClientId = "burgerama";

        public static void Configure(IAppBuilder app)
        {
            Contract.Requires<ArgumentNullException>(app != null);

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(BurgeramaDbContext.Create);
            app.CreatePerOwinContext<BurgeramaUserManager>(BurgeramaUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"), // todo correct url
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/account/ExternalLogin"), // todo correct url
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(oAuthOptions);

            // External identity providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            app.UseGoogleAuthentication();
        }
    }
}
