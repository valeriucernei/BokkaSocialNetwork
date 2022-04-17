using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.User;

public class UserUpdatePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; } = String.Empty;
    
    [Required]
    [StringLength(32, MinimumLength = 6)]
    public string NewPassword { get; set; } = String.Empty;
}