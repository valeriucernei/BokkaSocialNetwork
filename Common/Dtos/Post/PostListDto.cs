namespace Common.Dtos.Post;

public class PostListDto
{
    public string? Title { get; set; } = String.Empty;
    public string? Content { get; set; } = String.Empty;
    public int? LikesCount { get; set; } = 0;
    public string? User { get; set; } = String.Empty;
}