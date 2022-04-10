namespace Domain.Models;

public sealed class Photo : BaseEntity
{
    public string Extension { get; set; } = String.Empty;

    public Guid PostId { get; set; } = Guid.Empty;
    public Post Post { get; set; } = new Post();
}