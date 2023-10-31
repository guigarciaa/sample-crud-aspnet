using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Infra.Data
{
    public static class DatabaseService
    {
        public static void AddInfraDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SampleCrudDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database:ConnectionString")));
        }
    }
}