using System;
using Moq;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Auth;
using PrazoPosts.Service.Core;
using Microsoft.Extensions.Configuration;
using Xunit;
using PrazoPosts.Dto;
using PrazoPosts.Model;

namespace PrazoPosts.Service.Tests
{
    public class AuthServiceTests
    {
        [Fact]
        public void ShouldAuthenticate()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
            var mapper = TestHelper.GetMapper();

            var authData = new AuthDTO
            {
                Email = "test@provider.com",
                Password = "pwd"
            };
            var key = "1a78sd8a7s8d7aysda87s23"; 
            configurationMock.Setup(x => x[It.IsAny<string>()]).Returns(() => key);
            userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(new User
            {
                Email = authData.Email
            });
            cryptoServiceMock.Setup(x => x.VerifyPassword(It.IsAny<string>(),It.IsAny<string>())).Returns(true);
            var sut = new AuthService(cryptoServiceMock.Object, userRepositoryMock.Object, configurationMock.Object, mapper);
            var result = sut.Authenticate(authData);
            Assert.NotNull(result);
            Assert.NotNull(result.User);
            Assert.Null(result.User.Password);
            Assert.Equal(authData.Email, result.User.Email);
            Assert.NotEmpty(result.Token);
        }
    }
}
