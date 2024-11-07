using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Ibrahim Elsayed",
                    Email = "Ibrahimelsayed3015@gmail.com",
                    UserName = "Ibrahimelsayed3015",
                    PhoneNumber = "01116191349"
                };

                await userManager.CreateAsync(User, "Pa$$w0rd");
            }
        }

    }
}
