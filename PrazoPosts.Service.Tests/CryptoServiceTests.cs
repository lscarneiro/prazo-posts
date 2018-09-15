using System;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace PrazoPosts.Service.Tests
{
    public class CryptoServiceTests
    {

        [Fact]
        public void CanEncryptPassword()
        {
            var cryptoSvc = new CryptoService(new PasswordHasher<object>());
            var input = "test";
            var result = cryptoSvc.Encrypt(input);

            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.NotSame(input, result);
        }

        [Fact]
        public void CanVerifyEncryptedPassword()
        {
            var cryptoSvc = new CryptoService(new PasswordHasher<object>());
            var input = "test";
            var encrypted = cryptoSvc.Encrypt(input);
            var result = cryptoSvc.VerifyPassword(encrypted, input);

            Assert.True(result);
        }

        [Fact]
        public void CannotVerifyWrongEncryptedPassword()
        {
            var cryptoSvc = new CryptoService(new PasswordHasher<object>());
            var input = "test";
            var encrypted = cryptoSvc.Encrypt(input + "wrong");
            var result = cryptoSvc.VerifyPassword(encrypted, input);

            Assert.False(result);
        }
    }
}
