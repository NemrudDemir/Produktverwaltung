using Microsoft.EntityFrameworkCore;
using Produktverwaltung.Repository.Pagination;
using System.Linq;

namespace Produktverwaltung.Repository.Extensions
{
    public static class DbSetExtensions
    {
        public static IQueryable<TEntity> PaginationQuery<TEntity>(this IQueryable<TEntity> queryable, PaginationParameter paginationParameter) where TEntity : class
        {
            return queryable.Skip(paginationParameter.PageNumber * paginationParameter.PageSize).Take(paginationParameter.PageSize);
        }
    }
}
