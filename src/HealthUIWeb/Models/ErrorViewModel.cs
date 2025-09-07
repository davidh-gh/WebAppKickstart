using System.Diagnostics.CodeAnalysis;

namespace HealthUIWeb.Models;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
