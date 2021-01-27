using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using News.Core.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace News.Core.Helper
{
    public static class InitDatabase
    {
        public static async Task Migrate(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager, IConfiguration configuration)
        {
            var roles = new List<string>()
            {
                "Admin"
            };

            foreach (var role in roles)
            {
                var identityRole = new IdentityRole() {Name = role};
                await roleManager.CreateAsync(identityRole);
            }

            var email = "amar.catovic@news.com";
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new User()
                {
                    UserName = "Administrator",
                    Email = email,
                    EmailConfirmed = true
                };

                var createdAdmin = await userManager.CreateAsync(admin, "Jakojakalozinka1");
                if (createdAdmin.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}