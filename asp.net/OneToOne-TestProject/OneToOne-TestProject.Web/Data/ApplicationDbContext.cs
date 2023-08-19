using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OneToOne_TestProject.Web.Entities;
using System.Reflection.Metadata;

namespace OneToOne_TestProject.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(e => e.Cart)
                .WithOne(e => e.Customer)
                .HasForeignKey<Cart>(e => e.CustomerId)
                .IsRequired();

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasNoKey(); // Configure IdentityUserLogin as keyless

            // Your other entity configurations

            base.OnModelCreating(modelBuilder);
        }

    }
}