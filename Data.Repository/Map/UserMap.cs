using System;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repository.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserCode).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.NotesField).IsRequired().HasMaxLength(500);
            builder.Property(x => x.CreatedBy).HasMaxLength(10);
            builder.Property(x => x.emailAddress).IsRequired().HasMaxLength(10);
        }
    }
}

