using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Core.Models.Domain;
using News.Persistence.Entity_Configuration;

namespace News.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Core.Models.Domain.News> News { get; set; }

        public DbSet<UserEditedNews> UserEditedNews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new NewsEntityConfiguration());
            builder.ApplyConfiguration(new UserEditedNewsConfiguration());
        }
    }
}
