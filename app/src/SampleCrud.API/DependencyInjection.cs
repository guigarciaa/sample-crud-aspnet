using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<IPersonService, PersonService>();
        }

        public static void AddLoggingInfrastructure(this IHostBuilder builder)
        {
            SerilogExtension.AddSerilog(builder);
        }

        public static void AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
                };
            });
        }
    }
}