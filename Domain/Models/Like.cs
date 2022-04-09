namespace Domain.Models;

public class Like : EntityBase
{
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    
    public Guid PostId { get; set; } = Guid.Empty;
    public Post Post { get; set; } = new Post();
    
    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = new User();
}