using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    public required string Username { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public string Role { get; set; } = "User";

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? JwtToken { get; set; }
}
