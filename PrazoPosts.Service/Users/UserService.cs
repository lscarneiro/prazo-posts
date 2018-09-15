using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Interfaces;
using PrazoPosts.Service.Users.Validation;
using ValidationException = PrazoPosts.Service.Exceptions.ValidationException;

namespace PrazoPosts.Service.Users
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        ICryptoService _cryptoService;

        public UserService(IUserRepository userRepository,
                           ICryptoService cryptoService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        }

        public void RegisterUser(UserDTO userData)
        {
            var validator = new RegisterUserValidator(_userRepository);
            var validationResult = validator.Validate(userData);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
            var user = new User
            {
                Name = userData.Name,
                Email = userData.Email,
                Password = _cryptoService.Encrypt(userData.Password)
            };
            _userRepository.Insert(user);
        }

        public UserDTO GetUser(string _id)
        {
            var user = _userRepository.GetById(_id);
            return user != null
                ? new UserDTO
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                }
                : null;
        }


    }
}
