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
    public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            // Seed data for Borrower entity
            builder.HasData(
                new Borrower { Id = 1, Name = "Borrower1", Code = "BR001" },
                new Borrower { Id = 2, Name = "Borrower2", Code = "BR002" },
                new Borrower { Id = 3, Name = "Borrower3", Code = "BR003" },
                new Borrower { Id = 4, Name = "Borrower4", Code = "BR004" },
                new Borrower { Id = 5, Name = "Borrower5", Code = "BR005" }
            );
        }
    }

}
