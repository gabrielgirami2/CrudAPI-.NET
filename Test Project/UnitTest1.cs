using CrudApi.Controllers;
using CrudApi.Models;
using CrudApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test_Project
{
    public class UnitTest1
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductsController _controller;

        // Construtor renomeado corretamente
        public UnitTest1()
        {
            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = "1", Name = "Product1", Price = 10.0M },
                new Product { Id = "2", Name = "Product2", Price = 20.0M }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);
        }
    }
}
