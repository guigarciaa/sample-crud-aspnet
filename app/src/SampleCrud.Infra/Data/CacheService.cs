using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleCrud.Infra.Data
{
    public static class CacheService
    {

        public static void AddInfraCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["REDIS"];
                options.InstanceName = "SampleCrud";
            });
        }
    }
}