using Newtonsoft.Json;
using ProductsApi.DTOs.Models;
using System.Text;
using Xunit;

namespace ProductsApi.Tests
{
    public class ProductsControllerTests : IClassFixture<ProductApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly ProductApplicationFactory _factory;

        public ProductsControllerTests(ProductApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetV1_ShouldReturnProducts()
        {
            // Arrange
            var response = await _client.GetAsync("/api/products?api-version=1");

            // Act
            response.EnsureSuccessStatusCode(); 

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
            Assert.NotNull(products);
            Assert.True(products.Count >= 0);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct()
        {
            // Arrange
            var response = await _client.GetAsync("/api/products/1");

            // Act
            response.EnsureSuccessStatusCode(); 

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductModel>(responseString);
            Assert.NotNull(product);
            Assert.Equal(1, product.Id); 
        }

        [Fact]
        public async Task Put_ShouldUpdateProductDescription()
        {
            // Arrange
            var updatedDescription = "Updated product description";
            var content = new StringContent(JsonConvert.SerializeObject(updatedDescription), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/products/1", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var updatedProduct = JsonConvert.DeserializeObject<ProductModel>(responseString);
            Assert.Equal("Updated product description", updatedProduct?.Description);
        }

        [Fact]
        public async Task GetV2_ShouldReturnPaginatedProducts()
        {
            // Arrange
            var limit = 10;
            var offset = 0;
            var response = await _client.GetAsync($"/api/products?limit={limit}&offset={offset}&api-version=2");

            // Act
            response.EnsureSuccessStatusCode();

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var paginatedProducts = JsonConvert.DeserializeObject<ProductsWithPaginationModel>(responseString);
            Assert.NotNull(paginatedProducts);
            Assert.True(paginatedProducts.Products.Count() <= limit);
        }
    }
}
