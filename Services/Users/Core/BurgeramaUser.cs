using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Burgerama.Services.Users.Core
{
    public class BurgeramaUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BurgeramaUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here

            return userIdentity;
        }
    }
}