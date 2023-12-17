using Prometheus;
using SampleCrud.API;
using SampleCrud.Application.Services;
using SampleCrud.Data.Cache;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Cache;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;
using SampleCrud.Infra.Configurations.Services;
using StackExchange.Redis;

namespace SampleCrud.Infra.Injector
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCache(configuration);
            // services.AddSingleton<IConnectionMultiplexer>(sp =>
            // {
            //     return ConnectionMultiplexer.Connect("sample_crud_app_redis_debug:6379, password=samplecrudredisapp, abortConnect=false");
            // });
            services.AddDataInfrastructure(configuration);
            services.AddHealthChecks()
                .AddCheck<HealthCheck>(nameof(HealthCheck))
                .ForwardToPrometheus();
            // services.AddHostedService<InsertWorker>();
        }

        public static IServiceCollection AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add all DB dependencies here */
            services.AddInfraDatabase(configuration);
            /* Add all Cache dependencies here */
            services.AddScoped<ICacheService, CacheService>();

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Services
            services.AddScoped<IPersonService, PersonService>();
            return services;
        }
    }
}