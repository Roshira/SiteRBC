using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Services;
using System.Threading.Tasks;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SiteRBC.Tests
{
    public class CreateTest
    {
        private SiteRBCContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<SiteRBCContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            return new SiteRBCContext(options);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public async Task CreateProductAsync_ShouldAddValidProduct()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ProductService(context);
            var product = new ReadyProduct
            {
                Id = 1,
                Name = "Test Product",
                Price = 100,
                height = 10,
                width = 20,
                length = 30,
                timeForBuild = 5,
                AdressImage = null
            };

            // Act
            var validationResults = ValidateModel(product);

            // Debug: Вивід помилок валідації
            if (validationResults.Any())
            {
                foreach (var error in validationResults)
                {
                    System.Diagnostics.Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

            var result = await service.CreateProductAsync(product);

            // Assert
            Assert.True(!validationResults.Any(), "Model validation failed");
            Assert.True(result);
            Assert.Equal(1, await context.Products.CountAsync());
        }

        [Fact]
        public async Task CreateProductAsync_ShouldReturnFalse_WhenProductIsNull()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ProductService(context);

            // Act
            var result = await service.CreateProductAsync(null);

            // Assert
            Assert.False(result);
        }
    }
}
