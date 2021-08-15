using Produktverwaltung.DataAccess.Entities;
using Produktverwaltung.Repository.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetCategories(PaginationParameter pagination);
        ValueTask<Category> GetCategory(int id);
        ValueTask<Category> PostCategory(Category category);
        ValueTask<Category> PatchCategory(Category category);
        Task DeleteCategory(Category category);
    }
}
