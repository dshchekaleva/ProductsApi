using Application.Products.Interfaces;
using Application.Products.UseCases;
using Domain.Entities;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductById, Product>
    {
        private readonly IProductsRepository _repo;

        public GetProductByIdHandler(IProductsRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            return await _repo.GetById<Product>(request.Id, cancellationToken);
        }
    }
}
