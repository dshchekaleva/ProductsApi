using Application.Models;
using Domain.Common;

namespace ProductsApi.DTOs.Models
{
    public class ProductsWithPaginationModel
    {
        public ProductModel[] Products { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
}
