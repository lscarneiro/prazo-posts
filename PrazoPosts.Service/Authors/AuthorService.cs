using System;
using System.Collections.Generic;
using AutoMapper;
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

        public void CreateAuthor(AuthorDTO authorData)
        {
            var validator = new CreateAuthorValidator();
            var validationResult = validator.Validate(authorData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
            var author = _mapper.Map<AuthorDTO, Author>(authorData);
            _authorRepository.Insert(author);
        }

        public void DeleteAuthor(string _id)
        {
            throw new NotImplementedException();
        }

        public AuthorDTO GetAuthor(string _id)
        {
            var author = _authorRepository.GetById(_id);
            return _mapper.Map<Author, AuthorDTO>(author);
        }

        public IList<AuthorDTO> GetAuthors()
        {
            var authors = _authorRepository.GetAll(null);
            return _mapper.Map<IList<Author>, IList<AuthorDTO>>(authors);
        }
    }
}
