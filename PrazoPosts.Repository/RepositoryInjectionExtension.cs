using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace PrazoPosts.Repository
{
    public static class RepositoryInjectionExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services, string connectionString, string databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddSingleton(database);
        }
    }
}
