using System;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace Burgerama.Common.Authentication.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            Contract.Requires<ArgumentNullException>(principal != null);

            return principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}
