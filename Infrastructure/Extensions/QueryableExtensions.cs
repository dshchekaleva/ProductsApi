using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WithPagination<T>(this IQueryable<T> query, int offset, int limit)
        {
            return query.Skip(offset).Take(limit);
        }

        public static async Task<PaginationInfo> GetPaginationInfo<T>(this IQueryable<T> query, int offset, int limit, CancellationToken token)
        {
            var totalCount = await query.CountAsync(token);
            var returnCount = await query.WithPagination(offset, limit).CountAsync(token);
            return new PaginationInfo
            {
                Limit = limit,
                Offset = offset,
                Returned = returnCount,
                Total = totalCount
            };
        }
    }
}
