using Application.Products.Interfaces;
using Application.Products.UseCases;
using Domain.Entities;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProducts, Product[]>
    {
        private readonly IProductsRepository _repo;

        public GetProductsHandler(IProductsRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product[]> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            return await _repo.Get<Product>(cancellationToken);
        }
    }
}
