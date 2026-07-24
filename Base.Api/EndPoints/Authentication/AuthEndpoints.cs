using Base.Api.EndPoints.Common;
using Base.Aplication.Services.Authentication;
using Base.Domain.Dtos.Authentication;

namespace Base.Api.EndPoints.Authentication;

public static class AuthEndpoints
{
    internal static void MapAuthEndpoints(
        this WebApplication webApp)
    {
        webApp.MapGroup("auth")
            .WithTags("AUTHENTICATION")
            .MapAuthGroupEndpoints();
    }


    private static void MapAuthGroupEndpoints(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(
            "/register",
            (
                    RegisterRequestDto request,
                    AuthService service) =>
                service.RegisterAsync(request)
                    .ToApiResult());


        builder.MapPost(
            "/login",
            (
                    LoginRequestDto request,
                    AuthService service) =>
                service.LoginAsync(request)
                    .ToApiResult());


        builder.MapPost(
            "/refresh-token",
            (
                    RefreshTokenRequestDto request,
                    AuthService service) =>
                service.RefreshTokenAsync(request)
                    .ToApiResult());


        builder.MapPost(
            "/logout",
            (
                    RefreshTokenRequestDto request,
                    AuthService service) =>
                service.LogoutAsync(
                        request.RefreshToken)
                    .ToApiResult());
    }
}