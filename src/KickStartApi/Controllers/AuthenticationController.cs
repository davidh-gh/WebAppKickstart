using KickStartApi.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace KickStartApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IConfiguration config) : ControllerBase
{
    // api/authentication/token
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] AuthenticateIn? data)
    {
        // This is a placeholder for authentication logic.
        // In a real application, you would validate the credentials and return a token or user information.

        if (data == null)
        {
            return BadRequest("Invalid authentication request.");
        }
        var user = ValidateCredentials(data);

        if (user == null)
        {
            return Unauthorized();
        }

        // Simulate successful authentication
        var token = GenerateToken(user);
        return Ok(token);
    }

    private static AuthenticateOut? ValidateCredentials(AuthenticateIn data)
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
            return new AuthenticateOut(1, data.Username);
        }

        if (data.Username.Equals("aaaa", StringComparison.Ordinal)
            && data.Password.Equals("1234", StringComparison.Ordinal))
        {
            return new AuthenticateOut(2, data.Username);
        }

        return null;
    }

    private string GenerateToken(AuthenticateOut user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                config.GetValue<string>("Authentication:SecretKey") ?? throw new ArgumentException("SecretKey is missing in configuration.")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString(CultureInfo.InvariantCulture)),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new ( JwtRegisteredClaimNames.Typ, "user")
        ];

        var token = new JwtSecurityToken(
            config.GetValue<string>("Authentication:Issuer"),
            config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow, // When this token becomes valid
            DateTime.UtcNow.AddMinutes(1), // When the token will expire
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}