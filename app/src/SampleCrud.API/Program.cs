using Prometheus;
using SampleCrud.API;
using SampleCrud.Infra.Injector;
using SampleCrud.Infra.Utils;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbInfrastructure(builder.Configuration);

builder.Host.AddSerilogLogging(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck<HealthCheck>(nameof(HealthCheck))
    .ForwardToPrometheus();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpMetrics();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapMetrics();

app.Run();

public partial class Program { }
