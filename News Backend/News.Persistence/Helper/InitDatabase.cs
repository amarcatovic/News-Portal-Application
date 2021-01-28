using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using News.Core.Models.Domain;

namespace News.Persistence.Helper
{
    public static class InitDatabase
    {
        public static async Task Migrate(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager, IConfiguration configuration, IApplicationBuilder app)
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

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
            }
        }

        public static async Task SeedData(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(
                    new Category()
                    {
                        Name = "Agriculture"
                    },
                    new Category()
                    {
                        Name = "Aviation"
                    },
                    new Category()
                    {
                        Name = "Banking"
                    },
                    new Category()
                    {
                        Name = "Business"
                    },
                    new Category()
                    {
                        Name = "Community"
                    },
                    new Category()
                    {
                        Name = "Economics"
                    },
                    new Category()
                    {
                        Name = "Engineering"
                    },
                    new Category()
                    {
                        Name = "Entertainment"
                    },
                    new Category()
                    {
                        Name = "Federal"
                    },
                    new Category()
                    {
                        Name = "Games"
                    },
                    new Category()
                    {
                        Name = "Government"
                    },
                    new Category()
                    {
                        Name = "Immigration"
                    },
                    new Category()
                    {
                        Name = "Internet"
                    },
                    new Category()
                    {
                        Name = "Legal"
                    },
                    new Category()
                    {
                        Name = "Lifestyle"
                    },
                    new Category()
                    {
                        Name = "Local"
                    },
                    new Category()
                    {
                        Name = "Media"
                    },
                    new Category()
                    {
                        Name = "Medical"
                    },
                    new Category()
                    {
                        Name = "News"
                    },
                    new Category()
                    {
                        Name = "People"
                    },
                    new Category()
                    {
                        Name = "Science"
                    },
                    new Category()
                    {
                        Name = "Sport"
                    },
                    new Category()
                    {
                        Name = "Tech"
                    },
                    new Category()
                    {
                        Name = "Weather"
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
