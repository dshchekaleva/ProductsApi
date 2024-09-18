using Domain.Common;
using Domain.Common.Interfaces;

namespace Application.Common.Interfaces
{
    public interface IRepository
    {
        Task Update<TEntity>(TEntity entity, CancellationToken token) where TEntity : class;
        Task<TEntity[]> Get<TEntity>(CancellationToken token) where TEntity : class;
        Task<TEntity> GetById<TEntity>(int id, CancellationToken token) where TEntity : class, IId<int>;
        Task<(TEntity[] Entities, PaginationInfo Pagination)> Get<TEntity>(Pagination pagination, CancellationToken token)
       where TEntity : class, IId<int>;
    }
}
