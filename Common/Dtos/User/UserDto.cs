namespace Common.Dtos.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; }
    public DateTime RegisterDateTime { get; set; }
    
}