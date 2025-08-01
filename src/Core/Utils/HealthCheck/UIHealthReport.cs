using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Core.Utils.HealthCheck;

/*
 * Models for UI Client. These models represent an indirection between HealthChecks API and
 * UI Client in order to implement some features not present on HealthChecks of substitute
 * some properties etc.
 */
public class UiHealthReport(Dictionary<string, UiHealthReportEntry> entries, TimeSpan totalDuration)
{
    public UiHealthStatus Status { get; set; }
    public TimeSpan TotalDuration { get; set; } = totalDuration;

    // ReSharper disable once MemberCanBePrivate.Global - must be public for serialization
    public Dictionary<string, UiHealthReportEntry> Entries { get;  } = entries;

    public static UiHealthReport CreateFrom(HealthReport report, Func<Exception, string>? exceptionMessage = null)
    {
#pragma warning disable CA1062
        var uiReport = new UiHealthReport(new Dictionary<string, UiHealthReportEntry>(), report.TotalDuration)
#pragma warning restore CA1062
        {
            Status = (UiHealthStatus)report.Status,
        };

        foreach (var item in report.Entries)
        {
            var entry = new UiHealthReportEntry
            {
                Data = item.Value.Data,
                Description = item.Value.Description,
                Duration = item.Value.Duration,
                Status = (UiHealthStatus)item.Value.Status
            };

            if (item.Value.Exception != null)
            {
                var message = exceptionMessage == null ? item.Value.Exception.Message : exceptionMessage(item.Value.Exception);

                entry.Exception = message;
                entry.Description = item.Value.Description ?? message;
            }

            entry.Tags = item.Value.Tags;

            uiReport.Entries.Add(item.Key, entry);
        }

        return uiReport;
    }
}