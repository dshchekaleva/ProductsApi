using Domain.Entities;
using MediatR;

namespace Application.Products.UseCases
{
    public class UpdateDescription(int id, string description) : IRequest<Product>
    {
        public int Id { get; } = id;
        public string Description { get; } = description;
    }
}
