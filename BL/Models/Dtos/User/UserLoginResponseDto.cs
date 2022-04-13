namespace BL.Models.Dtos.User;

public class UserLoginResponseDto
{
    public string Token { get; set; } = String.Empty;
    public DateTime ValidTo { get; set; }
}