using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.Models.Domain;

namespace News.Persistence.Entity_Configuration
{
    public class UserEditedNewsConfiguration : IEntityTypeConfiguration<UserEditedNews>
    {
        public void Configure(EntityTypeBuilder<UserEditedNews> builder)
        {
            builder.HasKey(uen => new {uen.NewsId, uen.UserId, uen.DateEdited});

            builder.HasOne(uen => uen.User)
                .WithMany(u => u.UserEditedNews)
                .HasForeignKey(uen => uen.UserId);

            builder.HasOne(uen => uen.News)
                .WithMany(u => u.UserEditedNews)
                .HasForeignKey(uen => uen.NewsId);
        }
    }
}
