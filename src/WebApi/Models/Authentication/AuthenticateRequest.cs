using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Authentication;

internal sealed record AuthenticateRequest(
    [Required, MinLength(3), MaxLength(50)]
    string? Username,
    [Required, MinLength(3), MaxLength(50)]
    string? Password);
