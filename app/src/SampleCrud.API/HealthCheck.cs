using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SampleCrud.API
{
    public sealed class HealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}