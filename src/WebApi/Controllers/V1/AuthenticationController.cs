using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Code.Monitor;
using WebApi.Models.Authentication;

namespace WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController(IConfiguration config, ILogger<AuthenticationController> logger) : ControllerBase
{
    // api/authentication/token
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<AuthenticateResponse> Authenticate([FromBody] AuthenticateRequest data)
    {
        // This is a placeholder for authentication logic.
        // In a real application, you would validate the credentials and return a token or user information.

        LogMessages.LogAuthenticationRequest(logger, data?.Username ?? "NA", null);

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid authentication request.");
        }
        var user = ValidateCredentials(data!);

        if (user == null)
        {
            return Unauthorized();
        }

        // Simulate successful authentication
        var token = GenerateToken(user);
        return Ok(token);
    }

    private static AuthenticateUserData? ValidateCredentials(AuthenticateRequest data)
    {
        // Placeholder for credential validation logic
        // In a real application, you would check the username and password against a database or identity provider.
        if (string.IsNullOrEmpty(data.Username) || string.IsNullOrEmpty(data.Password))
        {
            throw new ArgumentException("Username and password must not be empty.");
        }

        if (data.Username.Equals("user", StringComparison.Ordinal)
            && data.Password.Equals("pass", StringComparison.Ordinal))
        {
            return new AuthenticateUserData(1, data.Username);
        }

        if (data.Username.Equals("aaaa", StringComparison.Ordinal)
            && data.Password.Equals("1234", StringComparison.Ordinal))
        {
            return new AuthenticateUserData(2, data.Username);
        }

        return null;
    }

    private AuthenticateResponse GenerateToken(AuthenticateUserData user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                config.GetValue<string>("Authentication:SecretKey") ?? throw new ArgumentException("SecretKey is missing in configuration.")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString(CultureInfo.InvariantCulture)),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new ( JwtRegisteredClaimNames.Typ, "user"),
            new(JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))
        ];

        var securityToken = new JwtSecurityToken(
            config.GetValue<string>("Authentication:Issuer"),
            config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow, // When this token becomes valid
            DateTime.UtcNow.AddMinutes(50), // When the token will expire
            signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new AuthenticateResponse(token);
    }
}