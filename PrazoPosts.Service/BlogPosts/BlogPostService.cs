using System;
using System.Collections.Generic;
using AutoMapper;
using MongoDB.Driver;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.BlogPosts.Validation;
using PrazoPosts.Service.Exceptions;
using System.Linq;
using MongoDB.Bson;

namespace PrazoPosts.Service.BlogPosts
{
    public class BlogPostService : IBlogPostService
    {
        IAuthorRepository _authorRepository;
        IBlogPostRepository _blogPostRepository;
        IMapper _mapper;
        public BlogPostService(IAuthorRepository authorRepository,
                               IBlogPostRepository blogPostRepository,
                               IMapper mapper)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _blogPostRepository = blogPostRepository ?? throw new ArgumentNullException(nameof(blogPostRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void CreateBlogPost(string userId, BlogPostDTO blogPostData)
        {
            var validator = new CreateUpdateBlogPostValidator();
            var validationResult = validator.Validate(blogPostData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var author = _authorRepository.GetByUserIdAndId(userId, blogPostData.AuthorId);
            if (author == null || (author != null && author.UserId != userId)) throw new NotFoundException("Autor não encontrado");

            var post = _mapper.Map<BlogPostDTO, BlogPost>(blogPostData);
            post.UserId = userId;
            _blogPostRepository.Insert(post);
        }

        public void DeleteBlogPost(string userId, string _id)
        {
            var post = _blogPostRepository.GetByUserIdAndId(userId, _id);
            if (post == null) throw new NotFoundException("Post não encontrado");
            _blogPostRepository.Delete(_id);
        }

        public BlogPostDTO GetBlogPost(string userId, string _id)
        {
            var post = _blogPostRepository.GetByUserIdAndId(userId, _id);
            if (post == null) throw new NotFoundException("Post não encontrado");
            var author = _authorRepository.GetByUserIdAndId(userId, post.AuthorId);
            var postDto = _mapper.Map<BlogPost, BlogPostDTO>(post);
            postDto.Author = _mapper.Map<Author, AuthorDTO>(author);
            return postDto;
        }

        public IList<BlogPostDTO> GetBlogPosts(string userId)
        {

            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            var filter = Builders<BlogPost>.Filter.Eq("UserId", userId);
            var posts = _blogPostRepository.GetAll(filter);
            var postsDto = _mapper.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostDTO>>(posts);
            return postsDto.Select(a =>
            {
                var author = _authorRepository.GetByUserIdAndId(userId, a.AuthorId);
                a.Author = _mapper.Map<Author, AuthorDTO>(author);
                return a;
            }).ToList();
        }

        public void UpdateBlogPost(string userId, string _id, BlogPostDTO blogPostData)
        {
            var validator = new CreateUpdateBlogPostValidator();
            var validationResult = validator.Validate(blogPostData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
            var post = _blogPostRepository.GetByUserIdAndId(userId, _id);
            if (post == null) throw new NotFoundException("Post não encontrado");
            var author = _authorRepository.GetByUserIdAndId(userId, blogPostData.AuthorId);
            if (author == null) throw new NotFoundException("Autor não encontrado");
            post = _mapper.Map<BlogPostDTO, BlogPost>(blogPostData);
            post.Id = ObjectId.Parse(_id);
            post.UserId = userId;
            _blogPostRepository.Update(_id, post);
        }
    }
}
