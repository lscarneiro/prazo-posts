using System;
using System.Collections.Generic;
using AutoMapper;
using MongoDB.Driver;
using Moq;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Authors;
using PrazoPosts.Service.Authors.Validation;
using PrazoPosts.Service.Core;
using Xunit;

namespace PrazoPosts.Service.Tests
{
    public class AuthorServiceTests
    {
        [Fact]
        public void ShouldGetAuthorById()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            var id = "5b9d8e952e6adf8005dbcf17";
            var expected = "TestSubject";
            blogPostRepositoryMock.Setup(x => x.PostCountByAuthor(It.IsAny<string>())).Returns(() => 0);
            authorRepositoryMock.Setup(x => x.GetByFilter(It.IsAny<FilterDefinition<Author>>())).Returns(new Author
            {
                Name = expected
            });
            var mapper = TestHelper.GetMapper();
            var sut = new AuthorService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            var result = sut.GetAuthor("12345", id);
            Assert.NotNull(result);
            Assert.Equal(expected, result.Name);
        }
        [Fact]
        public void ShouldGetAuthors()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            blogPostRepositoryMock.Setup(x => x.PostCountByAuthor(It.IsAny<string>())).Returns(() => 0);
            authorRepositoryMock.Setup(x => x.GetAll(It.IsAny<FilterDefinition<Author>>())).Returns(() => new List<Author>
            {
                new Author { Name = "Test1"},
                new Author { Name = "Test2"},
                new Author { Name = "Test3"}
            });
            var mapper = TestHelper.GetMapper();
            var sut = new AuthorService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            var result = sut.GetAuthors("12345");
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void ShouldCreateAuthor()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            var authorData = new AuthorDTO
            {
                Name = "Test1",
            };

            var mapper = TestHelper.GetMapper();
            var sut = new AuthorService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            sut.CreateAuthor("12345", authorData);
        }

        [Fact]
        public void ShouldAcceptValidAuthorDataForCreate()
        {

            var authorData = new AuthorDTO
            {
                Name = "Test1",
            };

            var sut = new CreateAuthorValidator();
            var result = sut.Validate(authorData);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldNotAcceptInvalidAuthorDataForCreate()
        {
            var authorData = new AuthorDTO
            {
                Name = "",
            };

            var sut = new CreateAuthorValidator();
            var result = sut.Validate(authorData);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void ShouldDeleteAuthor()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();

            var mapper = TestHelper.GetMapper();
            var sut = new AuthorService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            sut.DeleteAuthor("12345", "5b9d8e952e6adf8005dbcf17");
        }
    }
}
