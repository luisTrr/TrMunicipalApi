using System.Net;
using Base.Domain.Dtos.Authentication;
using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Authentication;
using Base.Domain.Responses;

namespace Base.Aplication.Services.Authentication;

public class AuthService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService)
{
    public async Task<Result<UserResponseDto>> RegisterAsync(
        RegisterRequestDto request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return Result<UserResponseDto>.Failure(
                new List<string>
                {
                    "Las contraseñas no coinciden."
                },
                HttpStatusCode.BadRequest);
        }

        if (await userRepository.ExistsByEmailAsync(request.Email))
        {
            return Result<UserResponseDto>.Failure(
                new List<string>
                {
                    "El correo electrónico ya está registrado."
                },
                HttpStatusCode.Conflict);
        }

        if (await userRepository.ExistsByUsernameAsync(request.Username))
        {
            return Result<UserResponseDto>.Failure(
                new List<string>
                {
                    "El nombre de usuario ya está registrado."
                },
                HttpStatusCode.Conflict);
        }

        var passwordHash =
            passwordHasher.Hash(request.Password);

        var user = new UserModel(
            request.Username,
            request.Email,
            passwordHash);

        if (user.HasErrors())
        {
            return Result<UserResponseDto>.Failure(
                user.GetAllMessageErrors(),
                HttpStatusCode.BadRequest);
        }

        var createdUser =
            await userRepository.CreateAsync(user);

        var response = new UserResponseDto
        {
            Id = createdUser.Id,
            Username = createdUser.Username,
            Email = createdUser.Email,
            Roles = new List<string>()
        };

        return Result<UserResponseDto>.Success(
            response,
            HttpStatusCode.Created);
    }
    // ---
    public async Task<Result<LoginResponseDto>> LoginAsync(
    LoginRequestDto request)
{
    var user =
        await userRepository.GetByEmailAsync(
            request.Email);

    if (user == null)
    {
        return Result<LoginResponseDto>.Failure(
            new List<string>
            {
                "El correo o la contraseña son incorrectos."
            },
            HttpStatusCode.Unauthorized);
    }

    if (!user.IsActive)
    {
        return Result<LoginResponseDto>.Failure(
            new List<string>
            {
                "El usuario se encuentra inactivo."
            },
            HttpStatusCode.Unauthorized);
    }

    var passwordValid =
        passwordHasher.Verify(
            request.Password,
            user.PasswordHash);

    if (!passwordValid)
    {
        return Result<LoginResponseDto>.Failure(
            new List<string>
            {
                "El correo o la contraseña son incorrectos."
            },
            HttpStatusCode.Unauthorized);
    }

    var roles =
        await userRepository.GetRolesAsync(
            user.Id);

    var accessToken =
        tokenService.GenerateAccessToken(
            user,
            roles);

    var refreshToken =
        tokenService.GenerateRefreshToken();

    var refreshTokenModel =
        new RefreshTokenModel(
            user.Id,
            refreshToken.Token,
            refreshToken.ExpiresAt);

    var createdRefreshToken =
        await refreshTokenRepository.CreateAsync(
            refreshTokenModel);

    var response = new LoginResponseDto
    {
        AccessToken = accessToken.Token,

        RefreshToken = createdRefreshToken.Token,

        AccessTokenExpiresAt =
            accessToken.ExpiresAt,

        RefreshTokenExpiresAt =
            createdRefreshToken.ExpiresAt,

        User = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = roles.ToList()
        }
    };

    return Result<LoginResponseDto>.Success(
        response,
        HttpStatusCode.OK);
}
    
    // ----
    public async Task<Result<TokenResponseDto>> RefreshTokenAsync(
    RefreshTokenRequestDto request)
{
    var refreshToken =
        await refreshTokenRepository
            .GetByTokenAsync(request.RefreshToken);

    if (refreshToken == null)
    {
        return Result<TokenResponseDto>.Failure(
            new List<string>
            {
                "El refresh token no existe."
            },
            HttpStatusCode.Unauthorized);
    }

    if (!refreshToken.IsActive)
    {
        return Result<TokenResponseDto>.Failure(
            new List<string>
            {
                "El refresh token ha expirado o fue revocado."
            },
            HttpStatusCode.Unauthorized);
    }

    var user =
        await userRepository.GetByIdAsync(
            refreshToken.UserId);

    if (user == null || !user.IsActive)
    {
        return Result<TokenResponseDto>.Failure(
            new List<string>
            {
                "El usuario no existe o está inactivo."
            },
            HttpStatusCode.Unauthorized);
    }

    var roles =
        await userRepository.GetRolesAsync(
            user.Id);

    var accessToken =
        tokenService.GenerateAccessToken(
            user,
            roles);

    var newRefreshToken =
        tokenService.GenerateRefreshToken();

    await refreshTokenRepository.RevokeAsync(
        refreshToken.Token);

    var newRefreshTokenModel =
        new RefreshTokenModel(
            user.Id,
            newRefreshToken.Token,
            newRefreshToken.ExpiresAt);

    var createdRefreshToken =
        await refreshTokenRepository.CreateAsync(
            newRefreshTokenModel);

    var response = new TokenResponseDto
    {
        AccessToken = accessToken.Token,

        RefreshToken =
            createdRefreshToken.Token,

        AccessTokenExpiresAt =
            accessToken.ExpiresAt,

        RefreshTokenExpiresAt =
            createdRefreshToken.ExpiresAt
    };

    return Result<TokenResponseDto>.Success(
        response,
        HttpStatusCode.OK);
}
    // ----
    public async Task<Result<bool>> LogoutAsync(
        string refreshToken)
    {
        var token =
            await refreshTokenRepository
                .GetByTokenAsync(refreshToken);

        if (token == null)
        {
            return Result<bool>.Failure(
                new List<string>
                {
                    "El refresh token no existe."
                },
                HttpStatusCode.NotFound);
        }

        if (token.IsRevoked)
        {
            return Result<bool>.Failure(
                new List<string>
                {
                    "El refresh token ya fue revocado."
                },
                HttpStatusCode.BadRequest);
        }

        var revoked =
            await refreshTokenRepository
                .RevokeAsync(refreshToken);

        if (!revoked)
        {
            return Result<bool>.Failure(
                new List<string>
                {
                    "No se pudo cerrar la sesión."
                },
                HttpStatusCode.BadRequest);
        }

        return Result<bool>.Success(
            true,
            HttpStatusCode.OK);
    }
}
