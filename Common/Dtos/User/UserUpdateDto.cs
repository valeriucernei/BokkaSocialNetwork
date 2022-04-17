using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.User;

public class UserUpdateDto
{
    [StringLength(32, MinimumLength = 5)]
    public string Username { get; set; } = String.Empty;
    
    [StringLength(24, MinimumLength = 3)]
    public string FirstName { get; set; } = String.Empty;
    
    [MaxLength(24)]
    public string? LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; } = String.Empty;
}