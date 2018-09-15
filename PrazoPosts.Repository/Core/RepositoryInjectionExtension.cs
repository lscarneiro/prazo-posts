using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Repository.Core
{
    public static class RepositoryInjectionExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services, string connectionString, string databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddSingleton(database);

            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
