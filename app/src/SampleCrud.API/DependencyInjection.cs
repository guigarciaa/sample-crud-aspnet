using SampleCrud.Application.Services;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;
using SampleCrud.Infra.Data;
using SampleCrud.Infra.Extensions;

namespace SampleCrud.Infra.Injector
{
    public static class DependencyInjection
    {
        public static void AddDbInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add all DB dependencies here */
            services.AddInfraDatabase(configuration);
            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            // Services
            services.AddScoped<IPersonService, PersonService>();
        }

        public static void AddLoggingInfrastructure(this IHostBuilder builder)
        {
            SerilogExtension.AddSerilog(builder);
        }
    }
}