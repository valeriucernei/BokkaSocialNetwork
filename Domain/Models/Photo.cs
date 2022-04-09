namespace Domain.Models;

public class Photo : EntityBase
{
    public string Extension { get; set; } = String.Empty;

    public Guid PostId { get; set; } = Guid.Empty;
    public Post Post { get; set; } = new Post();
}