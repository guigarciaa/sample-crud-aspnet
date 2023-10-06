using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Infra.IoC
{
    public static class DatabaseService
    {
        public static void AddInfraDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            Console.WriteLine("DatabaseManagementService.AddInfraDatabase");
        }
    }
}