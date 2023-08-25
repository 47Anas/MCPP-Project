using Anas_sBookShelf.Entities;
using Anas_sBookShelf.Entities.Books;
using Anas_sBookShelf.Entities.Cutomer;
using Anas_sBookShelf.Entities.Uploader;
using Microsoft.EntityFrameworkCore;

namespace Anas_sBookShelf.EfCore
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<UploaderImage> UploaderImages { get; set; }
        public DbSet<CustomerImage> CustomerImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UploaderImage>().UseTpcMappingStrategy()
                .ToTable("UploaderImages");

            modelBuilder.Entity<CustomerImage>()
                .ToTable("CustomerImages");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }    
    }
}




