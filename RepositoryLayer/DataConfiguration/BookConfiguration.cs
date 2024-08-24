using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;

namespace RepositoryLayer.DataConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Seed data for Book entity
            builder.HasData(
                new Book { Id = 1, Title = "C#", ImageUrl = "/images/snpL6u.jpg", MaxCopies = 5, AvailableCopies = 3 },
                new Book { Id = 2, Title = "SqlServer", ImageUrl = "/images/snpL6u.jpg", MaxCopies = 4, AvailableCopies = 1 },
                new Book { Id = 3, Title = "ASP.NET Core", ImageUrl = "/images/snpL6u.jpg", MaxCopies = 6, AvailableCopies = 2 },
                new Book { Id = 4, Title = "Docker Tutorial", ImageUrl = "/images/snpL6u.jpg", MaxCopies = 7, AvailableCopies = 4 },
                new Book { Id = 5, Title = "Introduction to SignalR", ImageUrl = "/images/snpL6u.jpg", MaxCopies = 3, AvailableCopies = 0 }
            );
        }
    }

}
