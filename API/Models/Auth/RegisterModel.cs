using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth;

public class RegisterModel
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