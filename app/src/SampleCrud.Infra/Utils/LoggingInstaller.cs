using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SampleCrud.Infra.Utils
{
    public static class LoggingInstaller
    {
        public static IHostBuilder AddSerilogLogging(this IHostBuilder host, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            
            host.UseSerilog();

            return host;
        }
    }
}