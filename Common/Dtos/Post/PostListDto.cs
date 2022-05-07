namespace Common.Dtos.Post;

public class PostListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string? Content { get; set; } 
    public DateTime CreatedDateTime { get; set; }
    public Guid? Photo { get; set; }
    public string? PhotoExtension { get; set; }
    public int LikesCount { get; set; }
    public string Username { get; set; } = String.Empty;
    public Guid UserId { get; set; }
}