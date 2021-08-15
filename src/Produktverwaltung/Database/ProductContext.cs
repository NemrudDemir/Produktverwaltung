using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Produktverwaltung.Database.Models;
using Produktverwaltung.Settings;

namespace Produktverwaltung.Database
{
    public class ProductContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
        }
    }
}
