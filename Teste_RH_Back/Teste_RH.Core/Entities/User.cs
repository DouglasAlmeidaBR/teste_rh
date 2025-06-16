namespace Teste_RH.Core.Entities;

public class User
{
    public Guid Id { get; set; } = new();
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public virtual Company? Company { get; set; }

    public void SetPassword(string password)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}
