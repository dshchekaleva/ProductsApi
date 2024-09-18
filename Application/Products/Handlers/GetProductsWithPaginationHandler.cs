using Application.Models;
using Application.Products.Interfaces;
using Application.Products.UseCases;
using Domain.Entities;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductsWithPaginationHandler : IRequestHandler<GetProductsWithPagination, ProductsWithPagination>
    {
        private readonly IProductsRepository _repo;

        public GetProductsWithPaginationHandler(IProductsRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductsWithPagination> Handle(GetProductsWithPagination request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetProducts(request.Pagination, cancellationToken);
            return new ProductsWithPagination(result.Products, result.Pagination);
        }
    }
}
