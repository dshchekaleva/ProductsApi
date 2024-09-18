using Application.Common;
using Application.Models;
using MediatR;

namespace Application.Products.UseCases
{
    public class GetProductsWithPagination(Pagination pagination) : IRequest<ProductsWithPagination>
    {
        public Pagination Pagination { get; } = pagination;
    }
}
