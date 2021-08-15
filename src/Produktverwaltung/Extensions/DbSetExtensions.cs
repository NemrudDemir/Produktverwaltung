using Microsoft.EntityFrameworkCore;
using Produktverwaltung.Repository.Pagination;
using System.Linq;

namespace Produktverwaltung.Extensions
{
    public static class DbSetExtensions
    {
        public static IQueryable<TEntity> Pagination<TEntity>(this DbSet<TEntity> dbSet, PaginationParameter paginationParameter) where TEntity : class
        {
            return dbSet.Skip(paginationParameter.PageNumber * paginationParameter.PageSize).Take(paginationParameter.PageSize);
        }
    }
}
