using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.User;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
    
    [Required(ErrorMessage = "First Name is required")]
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}