﻿using Burgerama.Services.Users.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Burgerama.Services.Users.Api
{
    public class ApplicationUserManager : UserManager<BurgeramaUser>
    {
        public ApplicationUserManager(IUserStore<BurgeramaUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<BurgeramaUser>(context.Get<BurgeramaDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<BurgeramaUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<BurgeramaUser>(dataProtectionProvider.Create("Burgerama Identity"));
            }

            return manager;
        }
    }
}
