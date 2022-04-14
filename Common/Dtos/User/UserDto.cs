namespace Common.Dtos.User;

public class UserDto
{
    public string UserName { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime RegisterDateTime { get; set; }
    
}