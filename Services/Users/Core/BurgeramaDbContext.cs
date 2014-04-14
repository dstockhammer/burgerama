using Microsoft.AspNet.Identity.EntityFramework;

namespace Burgerama.Services.Users.Core
{
    public class BurgeramaDbContext : IdentityDbContext<BurgeramaUser>
    {
        public BurgeramaDbContext()
            : base("DefaultConnection")
        {
        }

        public static BurgeramaDbContext Create()
        {
            return new BurgeramaDbContext();
        }
    }
}