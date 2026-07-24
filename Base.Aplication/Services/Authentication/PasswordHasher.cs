using Base.Domain.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Base.Aplication.Services.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<UserModel> _hasher = new();

    public string Hash(string password)
    {
        return _hasher.HashPassword(
            null!,
            password);
    }

    public bool Verify(
        string password,
        string passwordHash)
    {
        return _hasher.VerifyHashedPassword(
                   null!,
                   passwordHash,
                   password)
               == PasswordVerificationResult.Success;
    }
}