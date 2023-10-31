using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.API.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<SampleCrudDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<SampleCrudDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=samplecrud;Username=postgres;Password=postgres");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<SampleCrudDbContext>();

            db.Database.EnsureCreated();
        });
    }
}