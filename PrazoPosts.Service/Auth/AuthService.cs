using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Repository;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Auth.Validation;
using PrazoPosts.Service.Core;
using PrazoPosts.Service.Exceptions;

namespace PrazoPosts.Service.Auth
{
    public class AuthService : IAuthService
    {
        ICryptoService _cryptoService;
        IUserRepository _userRepository;
        IConfiguration _config;
        IMapper _mapper;

        public AuthService(ICryptoService cryptoService, 
                           IUserRepository userRepository, 
                           IConfiguration configuration, 
                           IMapper mapper)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TokenDTO Authenticate(AuthDTO authData)
        {
            var validator = new AuthDataValidator();
            var validationResult = validator.Validate(authData);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var user = _userRepository.GetByEmail(authData.Email);

            if (user == null) throw new NotFoundException("Usuário não encontrado ou senha inválida");

            var valid = _cryptoService.VerifyPassword(user.Password, authData.Password);

            if (!valid) throw new NotFoundException("Usuário não encontrado ou senha inválida");

            var tokenData = new TokenDTO
            {
                User = _mapper.Map<User, UserDTO>(user),
                Token = GenerateToken(user)
            };
            return tokenData;
        }

        string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            var token = new JwtSecurityToken(_config["Token:Issuer"],
                                             _config["Token:Issuer"],
                                             claims,
                                             expires: DateTime.Now.AddMonths(3),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
