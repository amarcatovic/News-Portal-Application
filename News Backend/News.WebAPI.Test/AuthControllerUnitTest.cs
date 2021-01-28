using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using News.Core.Interfaces;
using News.Core.Models.Authentification;
using News.WebAPI.Controllers;
using Xunit;

namespace News.WebAPI.Test
{
    public class AuthControllerUnitTest
    {
        private readonly Mock<IAuthentificationService> _mockAuthService;
        private readonly Mock<HttpRequest> _mockRequest;
        private AuthController _controller;

        public AuthControllerUnitTest()
        {
            _mockAuthService = new Mock<IAuthentificationService>();
            _mockRequest = new Mock<HttpRequest>();
        }

        [Fact]
        public async Task AuthUserAsync_Returns_Ok()
        {
            // Arrange
            _mockAuthService.Setup(a => a.AuthenticateUserAsync(It.IsAny<LoginDto>()))
                .ReturnsAsync(() => new LoginResponseDto());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new AuthController(_mockAuthService.Object);

            // Act
            var response = (OkObjectResult)await _controller.AuthUserAsync(It.IsAny<LoginDto>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async Task AuthUserAsync_Returns_Unauthorized()
        {
            // Arrange
            _mockAuthService.Setup(a => a.AuthenticateUserAsync(It.IsAny<LoginDto>()))
                .ReturnsAsync(() => null);
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new AuthController(_mockAuthService.Object);

            // Act
            var response = (UnauthorizedObjectResult)await _controller.AuthUserAsync(It.IsAny<LoginDto>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, actual);
        }

        [Fact]
        public async Task AuthUserAsync_Returns_BadRequest()
        {
            // Arrange
            _mockAuthService.Setup(a => a.AuthenticateUserAsync(It.IsAny<LoginDto>()))
                .ReturnsAsync(() => throw new Exception());
            _mockRequest.Setup(m => m.Scheme).Returns("https");
            _mockRequest.Setup(m => m.Host).Returns(new HostString("news.io"));
            _mockRequest.Setup(m => m.Path).Returns(new PathString("/unit-test"));

            _controller = new AuthController(_mockAuthService.Object);

            // Act
            var response = (BadRequestObjectResult)await _controller.AuthUserAsync(It.IsAny<LoginDto>());
            var actual = (HttpStatusCode)response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, actual);
        }
    }
}
