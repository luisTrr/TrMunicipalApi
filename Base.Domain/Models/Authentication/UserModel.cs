using System.ComponentModel.DataAnnotations;

namespace Base.Domain.Models.Authentication;

public class UserModel : TraceModel
{
    public int Id { get; private set; }

    public string Username { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime LastModifiedByAt { get; private set; }
    public int LastModifiedBy { get; private set; }

    public UserModel(
        string username,
        string email,
        string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(username))
            AddError(new Exception(
                "El nombre de usuario es obligatorio."));

        if (username?.Length > 50)
            AddError(new Exception(
                "El nombre de usuario no puede superar los 50 caracteres."));

        if (string.IsNullOrWhiteSpace(email))
            AddError(new Exception(
                "El correo electrónico es obligatorio."));

        if (email?.Length > 150)
            AddError(new Exception(
                "El correo electrónico no puede superar los 150 caracteres."));

        if (string.IsNullOrWhiteSpace(passwordHash))
            AddError(new Exception(
                "El hash de la contraseña es obligatorio."));

        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public UserModel(
        int id,
        string username,
        string email,
        string passwordHash,
        bool isActive,
        DateTime createdAt,
        int createdBy,
        DateTime lastModifiedByAt,
        int lastModifiedBy)
    {
        if (id <= 0)
            AddError(new Exception(
                "El ID del usuario es inválido."));

        if (string.IsNullOrWhiteSpace(username))
            AddError(new Exception(
                "El nombre de usuario es obligatorio."));

        if (string.IsNullOrWhiteSpace(email))
            AddError(new Exception(
                "El correo electrónico es obligatorio."));

        if (string.IsNullOrWhiteSpace(passwordHash))
            AddError(new Exception(
                "El hash de la contraseña es obligatorio."));

        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = isActive;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModifiedByAt = lastModifiedByAt;
        LastModifiedBy = lastModifiedBy;
    }
}