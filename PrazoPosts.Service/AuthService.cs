using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrazoPosts.Repository;
using PrazoPosts.Repository.Interfaces;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Interfaces;

namespace PrazoPosts.Service
{
    public class AuthService : IAuthService
    {
        ICryptoService _cryptoService;
        IUserRepository _userRepository;
        IConfiguration _config;

        public AuthService(ICryptoService cryptoService,
                          IUserRepository userRepository,
                          IConfiguration configuration)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public bool Authenticate(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null) throw new UserNotFoundException("Usuário não encontrado ou senha inválida");

            var valid = _cryptoService.VerifyPassword(user.Password, password);

            if (!valid) throw new UserNotFoundException("Usuário não encontrado ou senha inválida");

            return true;
        }

        private string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Token:Issuer"],
                                             _config["Token:Issuer"],
                                             expires: DateTime.Now.AddMonths(3),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
