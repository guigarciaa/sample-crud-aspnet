using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCrud.Domain.Cache;
using SampleCrud.Infra.Configurations.Services;
using StackExchange.Redis;

namespace SampleCrud.Infra.Configurations.Services
{
    public static class CacheServiceConfiguration
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration["REDIS"] ?? "localhost:6379, abortConnect=false"); 
            });
            return services;
        }
    }
}