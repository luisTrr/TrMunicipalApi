using Base.Domain.Models.Authentication;

namespace Base.Aplication.Services.Authentication;

public class JwtTokenService : ITokenService
{
    public TokenResult GenerateAccessToken(
        UserModel user,
        IEnumerable<string> roles)
    {
        throw new NotImplementedException();
    }

    public RefreshTokenResult GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
}