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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProduktverwaltungContext _ctx;

        public CategoryRepository(ProduktverwaltungContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Category> GetCategories(PaginationParameter pagination)
        {
            return _ctx.Categories.AsNoTracking().PaginationQuery(pagination);
        }

        public async ValueTask<Category> GetCategory(int id)
        {
            return await _ctx.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async ValueTask<Category> PostCategory(Category category)
        {
            var entry = await _ctx.Categories.AddAsync(category);
            await _ctx.SaveChangesAsync();

            return entry.Entity;
        }

        public async ValueTask<Category> PatchCategory(Category category)
        {
            var entry = _ctx.Categories.Update(category);
            await _ctx.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task DeleteCategory(Category category)
        {
            _ctx.Categories.Remove(category);
            await _ctx.SaveChangesAsync();
        }
    }
}
