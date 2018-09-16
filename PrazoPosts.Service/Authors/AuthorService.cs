using System;
using System.Collections.Generic;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Authors.Validation;
using PrazoPosts.Service.Exceptions;
using System.Linq;

namespace PrazoPosts.Service.Authors
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository _authorRepository;
        IBlogPostRepository _blogPostRepository;
        IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository,
                             IBlogPostRepository blogPostRepository,
                             IMapper mapper)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _blogPostRepository = blogPostRepository ?? throw new ArgumentNullException(nameof(blogPostRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void CreateAuthor(string userId, AuthorDTO authorData)
        {
            var validator = new CreateUpdateAuthorValidator();
            var validationResult = validator.Validate(authorData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
            var author = _mapper.Map<AuthorDTO, Author>(authorData);
            author.UserId = userId;
            _authorRepository.Insert(author);
        }
        public void UpdateAuthor(string userId, string id, AuthorDTO authorData)
        {
            var validator = new CreateUpdateAuthorValidator();
            var validationResult = validator.Validate(authorData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
            var author = _mapper.Map<AuthorDTO, Author>(authorData);
            author.Id = ObjectId.Parse(id);
            author.UserId = userId;
            _authorRepository.Update(id, author);
        }
        public void DeleteAuthor(string userId, string _id)
        {
            var author = _authorRepository.GetById(_id);
            if (author == null) throw new UnauthorizedActionException();
            _blogPostRepository.DeleteByAuthorId(_id);
            _authorRepository.Delete(_id);
        }

        public AuthorDTO GetAuthor(string userId, string _id)
        {
            var filter = Builders<Author>.Filter.Eq("UserId", userId) & Builders<Author>.Filter.Eq("_id", ObjectId.Parse(_id));
            var author = _authorRepository.GetByFilter(filter);
            var authorDto = _mapper.Map<Author, AuthorDTO>(author);
            authorDto.PostCount = _blogPostRepository.PostCountByAuthor(author.Id.ToString());
            return authorDto;
        }

        public IList<AuthorDTO> GetAuthors(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            var filter = Builders<Author>.Filter.Eq("UserId", userId);
            var authors = _authorRepository.GetAll(filter);
            var authorsDto = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(authors);
            return authorsDto.Select(a =>
             {
                 a.PostCount = _blogPostRepository.PostCountByAuthor(a.Id);
                 return a;
             }).ToList();

        }

    }
}
