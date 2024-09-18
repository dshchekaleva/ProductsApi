using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ProductsApi.Tests
{
    public class ProductApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            var database = $"Test{Guid.NewGuid()}Database";
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
            {
                UpdateDbContext<AppDbContext>(services, database);
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    db.Database.EnsureCreated();

                    SeedData(db);
                }
            });          
        }

        private void UpdateDbContext<TDbContext>(IServiceCollection services, string database) where TDbContext : DbContext
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase(database));
        }

        private void SeedData(AppDbContext context)
        {
            if (context.Products.Any())
            {
                return; 
            }

            context.Products.AddRange(
                new Product { Id = 1, Name = "Product 1", ImgUri = "uri1", Price = 1, Description = "Description 1" },
                new Product { Id = 2, Name = "Product 2", ImgUri = "uri2", Price = 2, Description = "Description 2" },
                new Product { Id = 3, Name = "Product 3", ImgUri = "uri3", Price = 3, Description = "Description 3" }
            );

            context.SaveChanges();
        }
    }
}
