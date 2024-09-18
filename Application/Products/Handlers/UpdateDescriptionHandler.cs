using Application.Products.Interfaces;
using Application.Products.UseCases;
using Domain.Entities;
using MediatR;

namespace Application.Products.Handlers
{
    public class UpdateDescriptionHandler : IRequestHandler<UpdateDescription, Product>
    {
        private readonly IProductsRepository _repo;

        public UpdateDescriptionHandler(IProductsRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product> Handle(UpdateDescription request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetById<Product>(request.Id, cancellationToken);
            product.Description = request.Description;
            await _repo.Update(product, cancellationToken);
            return product;
        }
    }
}
