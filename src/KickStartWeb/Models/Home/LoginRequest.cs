using System.Diagnostics.CodeAnalysis;

namespace KickStartWeb.Models.Home;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public record LoginRequest
{
    public string? UserName { get; init; }
    public string? Password { get; init; }
}
