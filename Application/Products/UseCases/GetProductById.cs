using Domain.Entities;
using MediatR;

namespace Application.Products.UseCases
{
    public class GetProductById : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
