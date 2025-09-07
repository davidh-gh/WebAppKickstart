using System.Diagnostics.CodeAnalysis;

namespace KickStartWeb.Models.Home;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public record LoginResponse
{
    public string? Token { get; init; }
}
