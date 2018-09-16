using System;
using MongoDB.Bson;
using Moq;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Core;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Users;
using PrazoPosts.Service.Users.Validation;
using Xunit;

namespace PrazoPosts.Service.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void ShouldGetUserById()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            var mapper = TestHelper.GetMapper();
            var id = "abc123";
            var expected = "TestSubject";
            userRepositoryMock.Setup(x => x.GetById(It.Is<string>(s => s == id))).Returns(new User
            {
                Name = expected
            });
            var sut = new UserService(userRepositoryMock.Object, cryptoServiceMock.Object, mapper);
            var result = sut.GetUser(id);
            Assert.NotNull(result);
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public void ShouldNotGetWrongUser()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            var mapper = TestHelper.GetMapper();
            var sut = new UserService(userRepositoryMock.Object, cryptoServiceMock.Object, mapper);
            userRepositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns<User>(null);
            Assert.Throws<NotFoundException>(() => sut.GetUser("test"));
        }

        [Fact]
        public void ShouldRegisterUser()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            Mock<ICryptoService> cryptoServiceMock = new Mock<ICryptoService>();
            var mapper = TestHelper.GetMapper();

            var userData = new UserDTO
            {
                Name = "Test1",
                Email = "test1@provider.com",
                Password = "pwd",
                PasswordConfirmation = "pwd"
            };

            var sut = new UserService(userRepositoryMock.Object, cryptoServiceMock.Object, mapper);
            sut.RegisterUser(userData);
        }

        [Fact]
        public void ShouldAcceptValidUserDataForRegister()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(() => null);

            var userData = new UserDTO
            {
                Name = "Test1",
                Email = "test1@provider.com",
                Password = "pwd",
                PasswordConfirmation = "pwd"
            };

            var sut = new RegisterUserValidator(userRepositoryMock.Object);
            var result = sut.Validate(userData);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldNotAcceptInvalidUserDataForRegister()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(() => null);

            var userData = new UserDTO
            {
                Name = "",
                Email = "",
                Password = "pwd",
                PasswordConfirmation = "pwdd"
            };

            var sut = new RegisterUserValidator(userRepositoryMock.Object);
            var result = sut.Validate(userData);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

    }
}
