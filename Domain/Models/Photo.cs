namespace Domain.Models;

public sealed class Photo : BaseEntity
{
    public string Extension { get; set; } = String.Empty;

    public string PostId { get; set; } = String.Empty;
    public Post Post { get; set; } = new Post();
}