using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Moq;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.BlogPosts;
using PrazoPosts.Service.BlogPosts.Validation;
using Xunit;

namespace PrazoPosts.Service.Tests
{
    public class BlogPostServiceTests
    {
        [Fact]
        public void ShouldGetBlogPostById()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            var id = "5b9d8e952e6adf8005dbcf17";
            var expected = "TestSubject";
            blogPostRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new BlogPost
            {
                Title = expected
            });
            authorRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new Author
            {
                Name = "AuthorTest"
            });
            var mapper = TestHelper.GetMapper();
            var sut = new BlogPostService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            var result = sut.GetBlogPost("12345", id);
            Assert.NotNull(result);
            Assert.Equal(expected, result.Title);
        }
        [Fact]
        public void ShouldGetBlogPosts()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            authorRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new Author
            {
                Name = "AuthorTest"
            });
            blogPostRepositoryMock.Setup(x => x.GetAll(It.IsAny<FilterDefinition<BlogPost>>())).Returns(() => new List<BlogPost>
            {
                new BlogPost { Title = "Test1"},
                new BlogPost { Title = "Test2"},
                new BlogPost { Title = "Test3"}
            });
            var mapper = TestHelper.GetMapper();
            var sut = new BlogPostService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            var result = sut.GetBlogPosts("12345");
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void ShouldCreateBlogPost()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            authorRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new Author
            {
                Name = "AuthorTest",
                UserId = "12345"
            });
            var authorData = new BlogPostDTO
            {
                Title = "Test1",
                AuthorId = "5b9d8e952e6adf8005dbcf17",
                Content = "test content"
            };

            var mapper = TestHelper.GetMapper();
            var sut = new BlogPostService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            sut.CreateBlogPost("12345", authorData);
        }


        [Fact]
        public void ShouldUpdateBlogPost()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            authorRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new Author
            {
                Name = "AuthorTest"
            });
            blogPostRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new BlogPost
            {
                Title = "Test"
            });
            var authorData = new BlogPostDTO
            {
                Title = "Test2",
                AuthorId = "5b9d8e952e6adf8005dbcf21",
                Content = "test content 2"
            };

            var mapper = TestHelper.GetMapper();
            var sut = new BlogPostService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            sut.UpdateBlogPost("12345", "5b9d8e952e6adf8005dbcf17", authorData);
        }

        [Fact]
        public void ShouldAcceptValidBlogPostDataForCreate()
        {

            var authorData = new BlogPostDTO
            {
                Title = "Test2",
                AuthorId = "5b9d8e952e6adf8005dbcf21",
                Content = "test content 2"
            };

            var sut = new CreateUpdateBlogPostValidator();
            var result = sut.Validate(authorData);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldNotAcceptInvalidBlogPostDataForCreate()
        {
            var authorData = new BlogPostDTO
            {
                Title = "",
                AuthorId = "",
                Content = ""
            };

            var sut = new CreateUpdateBlogPostValidator();
            var result = sut.Validate(authorData);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void ShouldDeleteBlogPost()
        {
            Mock<IAuthorRepository> authorRepositoryMock = new Mock<IAuthorRepository>();
            Mock<IBlogPostRepository> blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            blogPostRepositoryMock.Setup(x => x.GetByUserIdAndId(It.IsAny<string>(), It.IsAny<string>())).Returns(new BlogPost());
            var mapper = TestHelper.GetMapper();
            var sut = new BlogPostService(authorRepositoryMock.Object, blogPostRepositoryMock.Object, mapper);
            sut.DeleteBlogPost("12345", "5b9d8e952e6adf8005dbcf17");
        }
    }
}
