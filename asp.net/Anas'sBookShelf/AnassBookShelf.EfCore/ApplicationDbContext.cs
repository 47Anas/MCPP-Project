using Anas_sBookShelf.Entities;
using Microsoft.EntityFrameworkCore;

namespace Anas_sBookShelf.EfCore
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }    
    }
}




