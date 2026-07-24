using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Base.Domain.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Base.Aplication.Services.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Base.Infrastructure.Database.EntityFramework.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(
        IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }


    public TokenResult GenerateAccessToken(
        UserModel user,
        IEnumerable<string> roles)
    {
        var expiresAt =
            DateTime.UtcNow.AddMinutes(
                _settings.AccessTokenExpirationMinutes);


        var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()),

            new(
                JwtRegisteredClaimNames.UniqueName,
                user.Username),

            new(
                JwtRegisteredClaimNames.Email,
                user.Email),

            new(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };


        foreach (var role in roles)
        {
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    role));
        }


        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _settings.Key));


        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);


        return new TokenResult
        {
            Token =
                new JwtSecurityTokenHandler()
                    .WriteToken(token),

            ExpiresAt = expiresAt
        };
    }


    public RefreshTokenResult GenerateRefreshToken()
    {
        var tokenBytes =
            RandomNumberGenerator.GetBytes(64);


        var token =
            Convert.ToBase64String(tokenBytes);


        var expiresAt =
            DateTime.UtcNow.AddDays(
                _settings.RefreshTokenExpirationDays);


        return new RefreshTokenResult
        {
            Token = token,

            ExpiresAt = expiresAt
        };
    }
}