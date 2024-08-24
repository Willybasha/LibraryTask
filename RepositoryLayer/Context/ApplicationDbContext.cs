using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataConfiguration;
using RepositoryLayer.Entities;
using RepositoryLayer.Model;

namespace RepositoryLayer.Context
{
    public class ApplicationDbContext  : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BorrowerConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        }
    }
}
