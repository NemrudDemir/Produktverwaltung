using Microsoft.EntityFrameworkCore;
using Produktverwaltung.DataAccess.Entities;

namespace Produktverwaltung.DataAccess
{
    public class ProduktverwaltungContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public ProduktverwaltungContext(DbContextOptions<ProduktverwaltungContext> options) : base(options)
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
