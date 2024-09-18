using Domain.Common;
using Domain.Entities;

namespace Application.Models
{
    public class ProductsWithPagination
    {
        public ProductsWithPagination(Product[] products, PaginationInfo pagination)
        {
            Products = products;
            Pagination = pagination;
        }

        public Product[] Products { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
}
