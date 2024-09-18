using Application.Common;
using Application.Products.Interfaces;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ProductsRepository(AppDbContext context) : BaseRepository(context), IProductsRepository
    {
        public async Task<(Product[] Products, PaginationInfo Pagination)> GetProducts(Pagination pagination, CancellationToken cancellationToken)
        {
            return await Get<Product>(pagination, cancellationToken);
        }

    }
}
