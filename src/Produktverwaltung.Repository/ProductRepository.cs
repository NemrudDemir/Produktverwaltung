using Microsoft.EntityFrameworkCore;
using Produktverwaltung.DataAccess;
using Produktverwaltung.DataAccess.Entities;
using Produktverwaltung.Repository.Extensions;
using Produktverwaltung.Repository.Interfaces;
using Produktverwaltung.Repository.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProduktverwaltungContext _ctx;

        public ProductRepository(ProduktverwaltungContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Product> GetProducts(PaginationParameter pagination)
        {
            return _ctx.Products.AsNoTracking().PaginationQuery(pagination);
        }

        public async ValueTask<Product> GetProduct(int id)
        {
            return await _ctx.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async ValueTask<Product> PostProduct(Product product)
        {
            var entry = await _ctx.Products.AddAsync(product);
            await _ctx.SaveChangesAsync();

            return entry.Entity;
        }

        public async ValueTask<Product> PatchProduct(Product product)
        {
            var entry = _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task DeleteProduct(Product product)
        {
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
        }
    }
}
