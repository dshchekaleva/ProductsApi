using Application.Common;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Common.Interfaces;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository : IRepository
    {
        private AppDbContext Context { get; }

        public BaseRepository(AppDbContext context)
        {
            Context = context;
        }

        public virtual async Task<TEntity[]> Get<TEntity>(CancellationToken token) where TEntity : class
        {
            var query = Context.Set<TEntity>().AsQueryable();
            return await query.ToArrayAsync(token);
        }

        public virtual async Task<TEntity> GetById<TEntity>(int id, CancellationToken token) where TEntity : class, IId<int>
        {
            var query = Context.Set<TEntity>().AsQueryable();
            return await query.SingleAsync(x => x.Id == id, token);
        }

        public virtual Task Update<TEntity>(TEntity entity, CancellationToken token) where TEntity : class
        {
            Context.Update(entity);
            return Context.SaveChangesAsync(token);
        }

        public virtual async Task<(TEntity[] Entities, PaginationInfo Pagination)> Get<TEntity>(Pagination pagination, CancellationToken token)
       where TEntity : class, IId<int>
        {
            var query = Context.Set<TEntity>().AsQueryable();
            var newQuery = query.OrderBy(x => x.Id);
            var result = await newQuery.WithPagination(pagination.Offset, pagination.Limit).ToArrayAsync(token);
            var paginationInfo = await newQuery.GetPaginationInfo(pagination.Offset, pagination.Limit, token);
            return (result, paginationInfo);
        }
    }
}
