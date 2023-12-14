using Prometheus;
using SampleCrud.API;
using SampleCrud.API.Workers;
using SampleCrud.Infra.Injector;
using SampleCrud.Infra.Utils;
// using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureServices(builder.Configuration);

builder.Host.AddSerilogLogging(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseSerilogRequestLogging();

app.UseHttpMetrics();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapMetrics();

app.Run();

public partial class Program { }
