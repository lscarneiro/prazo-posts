using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Auth;
using PrazoPosts.Service.Core;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Users.Validation;
using ValidationException = PrazoPosts.Service.Exceptions.ValidationException;

namespace PrazoPosts.Service.Users
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        ICryptoService _cryptoService;
        IAuthService _authService;
        IMapper _mapper;

        public UserService(IUserRepository userRepository,
                           ICryptoService cryptoService,
                           IAuthService authService,
                           IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TokenDTO RegisterUser(UserDTO userData)
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
            var auth = new AuthDTO
            {
                Email = userData.Email,
                Password = userData.Password
            };
            return _authService.Authenticate(auth);
        }

        public UserDTO GetUser(string _id)
        {
            var user = _userRepository.GetById(_id);
            if (user == null) throw new NotFoundException("Usuário não encontrado");
            user.Password = null;
            return _mapper.Map<User, UserDTO>(user);
        }


    }
}
