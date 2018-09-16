using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PrazoPosts.Repository.Core;
using PrazoPosts.Service.Auth;
using PrazoPosts.Service.Authors;
using PrazoPosts.Service.BlogPosts;
using PrazoPosts.Service.Users;

namespace PrazoPosts.Service.Core
{
    public static class ServiceInjectionExtension
    {
        public static void AddServiceInjection(this IServiceCollection services, string connectionString, string databaseName)
        {
            services.AddRepositoryServices(connectionString, databaseName);
            services.AddTransient<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddAutoMapper();

            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBlogPostService, BlogPostService>();
        }
    }
}
