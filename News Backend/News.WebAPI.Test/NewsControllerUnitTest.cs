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
using News.Core.Models.Dtos.News;
using News.WebAPI.Controllers;
using Xunit;

namespace News.WebAPI.Test
{
    public class NewsControllerUnitTest
    {
        private readonly Mock<INewsService> _mockNewsService;
        private readonly Mock<HttpRequest> _mockRequest;
        private NewsController _controller;

        public NewsControllerUnitTest()
        {
            _mockNewsService = new Mock<INewsService>();
            _mockRequest = new Mock<HttpRequest>();
        }

        [Fact]
        public async Task GetAllNewsAsync_Returns_Ok()
        {
            // Arrange
            _mockNewsService.Setup(a => a.GetAllNewsAsync())
                .ReturnsAsync(() => new List<NewsReadDto>(){new NewsReadDto()});
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new NewsController(_mockNewsService.Object);

            // Act
            var response = (OkObjectResult)await _controller.GetAllNewsAsync();
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async Task GetAllNewsAsync_Returns_NoContent()
        {
            // Arrange
            _mockNewsService.Setup(a => a.GetAllNewsAsync())
                .ReturnsAsync(() => new List<NewsReadDto>());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new NewsController(_mockNewsService.Object);

            // Act
            var response = (NoContentResult)await _controller.GetAllNewsAsync();
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, actual);
        }

        [Fact]
        public async Task GetAllNewsAsync_Returns_BadRequest()
        {
            // Arrange
            _mockNewsService.Setup(a => a.GetAllNewsAsync())
                .ReturnsAsync(() => null);
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new NewsController(_mockNewsService.Object);

            // Act
            var response = (BadRequestObjectResult)await _controller.GetAllNewsAsync();
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, actual);
        }

        [Fact]
        public async Task GetNewsByIdAsync_Returns_Ok()
        {
            // Arrange
            _mockNewsService.Setup(a => a.GetNewsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new NewsReadDto());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new NewsController(_mockNewsService.Object);

            // Act
            var response = (OkObjectResult)await _controller.GetNewsByIdAsync(It.IsAny<int>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async Task GetNewsByIdAsync_Returns_NotFound()
        {
            // Arrange
            _mockNewsService.Setup(a => a.GetNewsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new NewsController(_mockNewsService.Object);

            // Act
            var response = (NotFoundResult)await _controller.GetNewsByIdAsync(It.IsAny<int>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, actual);
        }

        [Fact]
        public async Task GetNewsByIdAsync_Returns_BadRequest()
        {
            // Arrange
            _mockNewsService.Setup(a => a.GetNewsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new NewsController(_mockNewsService.Object);

            // Act
            var response = (BadRequestObjectResult)await _controller.GetNewsByIdAsync(It.IsAny<int>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, actual);
        }

    }
}
