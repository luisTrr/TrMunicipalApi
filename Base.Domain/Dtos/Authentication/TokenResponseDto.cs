namespace Base.Domain.Dtos.Authentication;

public class TokenResponseDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime AccessTokenExpiresAt { get; set; }

    public DateTime RefreshTokenExpiresAt { get; set; }
}