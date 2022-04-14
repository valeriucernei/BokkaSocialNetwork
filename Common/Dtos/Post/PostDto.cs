namespace Common.Dtos.Post;

public class PostDto
{
    public Guid? Id { get; set; } = Guid.Empty;
    public string? Title { get; set; } = String.Empty;
    public string? Content { get; set; } = String.Empty;
    public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
    public int? LikesCount { get; set; } = 0;
    public Guid? UserId { get; set; } = Guid.Empty;
}