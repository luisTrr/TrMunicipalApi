using Base.Domain.Models.Authentication;

namespace Base.Aplication.Services.Authentication;

public interface ITokenService
{
    TokenResult GenerateAccessToken(
        UserModel user,
        IEnumerable<string> roles);

    RefreshTokenResult GenerateRefreshToken();
}

public class TokenResult
{
    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }
}

public class RefreshTokenResult
{
    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }
}