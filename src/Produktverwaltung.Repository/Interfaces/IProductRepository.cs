using Produktverwaltung.DataAccess.Entities;
using Produktverwaltung.Repository.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Repository.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts(PaginationParameter pagination);
        ValueTask<Product> GetProduct(int id);
        ValueTask<Product> PostProduct(Product product);
        ValueTask<Product> PatchProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
