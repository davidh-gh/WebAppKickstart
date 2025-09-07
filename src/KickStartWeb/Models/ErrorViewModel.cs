using System.Diagnostics.CodeAnalysis;

namespace KickStartWeb.Models;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
