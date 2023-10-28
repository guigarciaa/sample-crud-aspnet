using Prometheus.DotNetRuntime;
using SampleCrud.Application.Services;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;
using SampleCrud.Infra.Data;
using SampleCrud.Infra.Extensions;

namespace SampleCrud.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddDbInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add all DB dependencies here */
            services.AddInfraDatabase(configuration);
            // Scopes
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonServices>();
        }

        public static void AddLoggingInfrastructure(this IHostBuilder builder)
        {
            SerilogExtension.AddSerilog(builder);
        }
    }
}