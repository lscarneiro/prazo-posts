﻿using System;
using Microsoft.Extensions.DependencyInjection;
using PrazoPosts.Repository;

namespace PrazoPosts.Service
{
    public static class ServiceInjectionExtension
    {
        public static void AddServiceInjection(this IServiceCollection services, string connectionString, string databaseName)
        {
            services.AddRepositoryServices(connectionString, databaseName);
        }
    }
}