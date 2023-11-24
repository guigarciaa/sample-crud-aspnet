using Microsoft.Extensions.Hosting;
using Serilog;

namespace SampleCrud.Infra.Extensions
{
    public static class SerilogExtension
    {
        public static void AddSerilog(IHostBuilder hostBuilder) {
            hostBuilder.UseSerilog((context, services, configuration) =>
            {
                if (context.HostingEnvironment.IsProduction() || context.HostingEnvironment.IsStaging())
                {
                    configuration
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(
                            new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(context.Configuration["ELASTICSEARCH_URL"]))
                            {

                                IndexFormat = $"{context.Configuration["ApplicationName"]?.ToLower()}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                                AutoRegisterTemplate = true,
                                NumberOfReplicas = 1,
                                NumberOfShards = 1
                            })
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services);
                }
                else
                {
                    configuration
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services);
                }
            });
        }
    }
}