namespace KickStartWeb.Models.Home;

public record LoginRequest
{
        public string? UserName { get; init; }
        public string? Password { get; init; }
}