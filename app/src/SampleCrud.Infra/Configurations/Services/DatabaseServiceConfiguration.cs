using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Infra.Configurations.Services
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddInfraDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SampleCrudDbContext>(options =>
                options.UseNpgsql(configuration["DBHOST"]));

            return services;
        }
    }
}