namespace Base.Domain.Dtos.Authentication;

public class LoginResponseDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime AccessTokenExpiresAt { get; set; }

    public DateTime RefreshTokenExpiresAt { get; set; }

    public UserResponseDto User { get; set; }
}