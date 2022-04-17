using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.User;

public class UserLoginDto
{
    [DefaultValue("valera")]
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [DefaultValue("123321")]
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}