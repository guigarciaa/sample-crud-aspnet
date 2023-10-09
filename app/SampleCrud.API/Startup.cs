using SampleCrud.Application.Services;
using SampleCrud.Data.Repositories;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;

namespace SampleCrud.Infra.IoC
{
    public static class Startup
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            /* Add all DB dependencies here */
            services.AddInfraDatabase(configuration);
            // Scopes
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonServices>();
        }
    }
}