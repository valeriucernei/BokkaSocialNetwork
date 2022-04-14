namespace Common.Dtos.User;

public class UserLoginResponseDto
{
    public string token_type { get; set; } = String.Empty;
    public string access_token { get; set; } = String.Empty;
    public DateTime expires_at { get; set; }
}