using System.Security.Claims;

namespace Burgerama.Common.Authentication.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return null;

            var userId = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return userId == null ? null : userId.Value;
        }
    }
}
