using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;

namespace RepositoryLayer.DataConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Seed data for ApplicationUser entity
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(), // Primary key must be a string
                    UserName = "User1",
                    NormalizedUserName = "USER1",
                    PasswordHash = hasher.HashPassword(null, "1357@Mm"), // Example hashed password
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            ); ;
        }
    }

}
