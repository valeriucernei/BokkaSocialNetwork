namespace Common.Dtos.Post;

public class PostDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = String.Empty;
    public string? Content { get; set; } 
    public DateTime CreatedDateTime { get; set; } 
    public int LikesCount { get; set; }
    public Guid UserId { get; set; } = Guid.Empty;
    public string Username { get; set; } = String.Empty;
}