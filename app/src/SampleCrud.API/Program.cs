using Prometheus;
using SampleCrud.API;
using SampleCrud.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks()
    .AddCheck<HealthCheck>(nameof(HealthCheck))
    .ForwardToPrometheus();


builder.Host.AddLoggingInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseHttpMetrics();

app.UseAuthorization();

app.MapControllers();

app.MapMetrics();

app.Run();
