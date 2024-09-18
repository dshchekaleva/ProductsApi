namespace ProductsApi.DTOs.Models
{
    public record ProductModel(int Id, string Name, string ImgUri, decimal Price, string? Description);
}
