using System;
using Microsoft.AspNetCore.Identity;
using PrazoPosts.Model;

namespace PrazoPosts.Service.Core
{
    public class CryptoService : ICryptoService
    {
        IPasswordHasher<object> _passwordHasher;
        public CryptoService(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public string Encrypt(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hash, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hash, password);
            return result.Equals(PasswordVerificationResult.Success);
        }
    }
}
