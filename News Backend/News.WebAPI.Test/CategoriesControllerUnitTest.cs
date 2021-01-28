using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using News.Core.Interfaces;
using News.Core.Models.Authentification;
using News.Core.Models.Dtos.Category;
using News.WebAPI.Controllers;
using Xunit;

namespace News.WebAPI.Test
{
    public class CategoriesControllerUnitTest
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<HttpRequest> _mockRequest;
        private CategoriesController _controller;

        public CategoriesControllerUnitTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockRequest = new Mock<HttpRequest>();
        }

        [Fact]
        public async Task GetAllCategories_Returns_Ok()
        {
            // Arrange
            _mockCategoryService.Setup(a => a.GetAllCategoriesAsync())
                .ReturnsAsync(() => new List<CategoryReadDto>(){new CategoryReadDto()});
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new CategoriesController(_mockCategoryService.Object);

            // Act
            var response = (OkObjectResult)await _controller.GetAllCategoriesAsync();
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async Task GetAllCategories_Returns_NoContent()
        {
            // Arrange
            _mockCategoryService.Setup(a => a.GetAllCategoriesAsync())
                .ReturnsAsync(() => new List<CategoryReadDto>());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new CategoriesController(_mockCategoryService.Object);

            // Act
            var response = (NoContentResult)await _controller.GetAllCategoriesAsync();
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, actual);
        }

        [Fact]
        public async Task GetAllCategories_Returns_BadRequest()
        {
            // Arrange
            _mockCategoryService.Setup(a => a.GetAllCategoriesAsync())
                .ReturnsAsync(() => throw new Exception());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new CategoriesController(_mockCategoryService.Object);

            // Act
            var response = (BadRequestObjectResult)await _controller.GetAllCategoriesAsync();
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, actual);
        }

        [Fact]
        public async Task GetCategoryNewsAsync_Returns_Ok()
        {
            // Arrange
            _mockCategoryService.Setup(a => a.GetCategoryAndTheirNewsAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new CategoryReadAllNewsDto());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new CategoriesController(_mockCategoryService.Object);

            // Act
            var response = (OkObjectResult)await _controller.GetCategoryNewsAsync(It.IsAny<int>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async Task GetCategoryNewsAsync_Returns_NotFound()
        {
            // Arrange
            _mockCategoryService.Setup(a => a.GetCategoryAndTheirNewsAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new CategoriesController(_mockCategoryService.Object);

            // Act
            var response = (NotFoundResult)await _controller.GetCategoryNewsAsync(It.IsAny<int>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, actual);
        }

        [Fact]
        public async Task GetCategoryNewsAsync_Returns_BadRequest()
        {
            // Arrange
            _mockCategoryService.Setup(a => a.GetCategoryAndTheirNewsAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new CategoriesController(_mockCategoryService.Object);

            // Act
            var response = (BadRequestObjectResult)await _controller.GetCategoryNewsAsync(It.IsAny<int>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, actual);
        }
    }
}
