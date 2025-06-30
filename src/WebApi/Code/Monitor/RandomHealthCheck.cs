using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Cryptography;

namespace WebApi.Code.Monitor;

public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        int responseTimeInMs = RandomNumberGenerator.GetInt32(300);

        return responseTimeInMs switch
        {
            < 150 => Task.FromResult(HealthCheckResult.Healthy($"The response time is excellent ({responseTimeInMs}ms)")),
            < 200 => Task.FromResult(HealthCheckResult.Degraded($"The response time is greater than expected ({responseTimeInMs}ms)")),
            _ => Task.FromResult(HealthCheckResult.Unhealthy($"The response time is unacceptable ({responseTimeInMs}ms)"))
        };
    }
}