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

namespace PrazoPosts.Service.Authors
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository _authorRepository;
        IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository,
                            IMapper mapper)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void CreateAuthor(string userId, AuthorDTO authorData)
        {
            var validator = new CreateAuthorValidator();
            var validationResult = validator.Validate(authorData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
            var author = _mapper.Map<AuthorDTO, Author>(authorData);
            author.UserId = userId;
            _authorRepository.Insert(author);
        }

        public void DeleteAuthor(string userId, string _id)
        {
            throw new NotImplementedException();
        }

        public AuthorDTO GetAuthor(string userId, string _id)
        {
            var filter = Builders<Author>.Filter.Eq("UserId", userId) & Builders<Author>.Filter.Eq("_id", ObjectId.Parse(_id));
            var author = _authorRepository.GetByFilter(filter);
            return _mapper.Map<Author, AuthorDTO>(author);
        }

        public IList<AuthorDTO> GetAuthors(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            var filter = Builders<Author>.Filter.Eq("UserId", userId);
            var authors = _authorRepository.GetAll(filter);
            return _mapper.Map<IList<Author>, IList<AuthorDTO>>(authors);
        }
    }
}
