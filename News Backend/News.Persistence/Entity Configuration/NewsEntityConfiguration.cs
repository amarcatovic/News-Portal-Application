using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace News.Persistence.Entity_Configuration
{
    public class NewsEntityConfiguration : IEntityTypeConfiguration<Core.Models.Domain.News>
    {
        public void Configure(EntityTypeBuilder<Core.Models.Domain.News> builder)
        {
            builder.HasKey(n => n.CategoryId);

            builder.HasIndex(n => n.Title);

            builder.Property(n => n.Title)
                .IsRequired();

            builder.Property(n => n.Content)
                .IsRequired();

            builder.HasOne(n => n.Category)
                .WithMany(c => c.News)
                .HasForeignKey(n => n.CategoryId);


            builder.HasOne(n => n.User)
                .WithMany(u => u.News)
                .HasForeignKey(n => n.UserId);
        }
    }
}
