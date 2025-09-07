namespace Core.Utils.HealthCheck;

public class UiHealthReportEntry
{
    public IReadOnlyDictionary<string, object> Data { get; set; } = null!;
    public string? Description { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Exception { get; set; }
    public UiHealthStatus Status { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}
