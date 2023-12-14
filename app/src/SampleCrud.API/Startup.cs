using Prometheus;
using SampleCrud.API;
using SampleCrud.API.Workers;
using SampleCrud.Application.Services;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;
using SampleCrud.Infra.Data;
using SampleCrud.Infra.Utils;

namespace SampleCrud.Infra.Injector
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDataInfrastructure(configuration);
            services.AddInfraCache(configuration);
            services.AddHostedService<InsertWorker>();
            services.AddHealthChecks()
                .AddCheck<HealthCheck>(nameof(HealthCheck))
                .ForwardToPrometheus();
        }

        public static IServiceCollection AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add all DB dependencies here */
            services.AddInfraDatabase(configuration);
            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            // Services
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}