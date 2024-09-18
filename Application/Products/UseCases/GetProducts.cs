using Domain.Entities;
using MediatR;

namespace Application.Products.UseCases
{
    public class GetProducts : IRequest<Product[]>
    {
    }
}
