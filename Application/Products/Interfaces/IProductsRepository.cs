using Application.Common;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;

namespace Application.Products.Interfaces
{
    public interface IProductsRepository : IRepository
    {
        Task<(Product[] Products, PaginationInfo Pagination)> GetProducts(Pagination pagination, CancellationToken cancellationToken);
    }
}
