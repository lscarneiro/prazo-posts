using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PrazoPosts.Repository.Core;
using PrazoPosts.Service.Auth;
using PrazoPosts.Service.Authors;
using PrazoPosts.Service.Users;

namespace PrazoPosts.Service.Core
{
    public static class ServiceInjectionExtension
    {
        public static void AddServiceInjection(this IServiceCollection services, string connectionString, string databaseName)
        {
            services.AddRepositoryServices(connectionString, databaseName);
            services.AddSingleton<IPasswordHasher<object>>(new PasswordHasher<object>());
            services.AddAutoMapper();

            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAuthorService, AuthorService>();
        }
    }
}
