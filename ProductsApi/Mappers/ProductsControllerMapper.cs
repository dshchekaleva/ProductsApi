using Application.Models;
using AutoMapper;
using Domain.Entities;
using ProductsApi.DTOs.Models;

namespace ProductsApi.Mappers
{
    public class ProductsControllerMapper : Profile
    {
        public ProductsControllerMapper()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductsWithPagination, ProductsWithPaginationModel>()
                .ForMember(x => x.Products, opt => opt.MapFrom(o => o.Products != null ? o.Products.Select(x => new ProductModel(x.Id, x.Name, x.ImgUri, x.Price, x.Description)) : null));
        }
    }
}
