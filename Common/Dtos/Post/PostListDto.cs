namespace Common.Dtos.Post;

public class PostListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string? Content { get; set; }
    //public ICollection<Photo> Photos { get; set; } = new Collection<Photo>();
    //public ICollection<Like> Likes { get; set; } = new Collection<Like>();
    public string User { get; set; } = String.Empty;
    public DateTime CreatedDateTime { get; set; }
}