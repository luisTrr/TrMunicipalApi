namespace Base.Domain.Models.Authentication;

public class RefreshTokenModel : TraceModel
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public string Token { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    public bool IsRevoked => RevokedAt.HasValue;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsActive => !IsRevoked && !IsExpired;
    
    public int CreatedBy { get; private set; }
    
    public DateTime LastModifiedByAt { get; private set; }
    
    public int LastModifiedBy { get; private set; }

    public RefreshTokenModel(
        int userId,
        string token,
        DateTime expiresAt)
    {
        if (userId <= 0)
            AddError(new Exception(
                "El ID del usuario es inválido."));

        if (string.IsNullOrWhiteSpace(token))
            AddError(new Exception(
                "El token es obligatorio."));

        if (expiresAt <= DateTime.UtcNow)
            AddError(new Exception(
                "La fecha de expiración debe ser futura."));

        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
    }

    public RefreshTokenModel(
        int id,
        int userId,
        string token,
        DateTime expiresAt,
        DateTime createdAt,
        DateTime? revokedAt,
        int createdBy,
        DateTime lastModifiedByAt,
        int lastModified)
    {
        if (id <= 0)
            AddError(new Exception(
                "El ID del refresh token es inválido."));

        if (userId <= 0)
            AddError(new Exception(
                "El ID del usuario es inválido."));

        if (string.IsNullOrWhiteSpace(token))
            AddError(new Exception(
                "El token es obligatorio."));

        Id = id;
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
        RevokedAt = revokedAt;
        CreatedBy = createdBy;
        LastModifiedByAt = lastModifiedByAt;
        LastModifiedBy = lastModified;
    }


    public void Revoke()
    {
        if (IsRevoked)
        {
            AddError(new Exception(
                "El token ya fue revocado."));

            return;
        }

        RevokedAt = DateTime.UtcNow;
    }
}