using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Services;
using System.Threading.Tasks;
using Xunit;

namespace SiteRBC.Tests
{
    public class DeleteTest
    {
        private SiteRBCContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<SiteRBCContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            return new SiteRBCContext(options);
        }
        [Fact]
        public async Task DeleteProductAsync_ShouldRemoveProduct()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var product = new ReadyProduct
            {
                Id = 4,
                Name = "Test Product",
                Price = 1030,
                height = 110,
                width = 220,
                length = 330,
                timeForBuild = 35,
                AdressImage = null
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var service = new ProductService(context);

            // Act
            var result = await service.DeleteProductAsync(4);

            // Assert
            Assert.True(result);
            Assert.Equal(0, await context.Products.CountAsync());
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnFalse_WhenProductNotFound()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ProductService(context);

            // Act
            var result = await service.DeleteProductAsync(99);

            // Assert
            Assert.False(result);
        }

    }
}
