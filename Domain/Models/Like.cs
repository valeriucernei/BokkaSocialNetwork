using Domain.Models.Auth;

namespace Domain.Models;

public sealed class Like : BaseEntity
{
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    
    public string PostId { get; set; } = String.Empty;
    public Post Post { get; set; } = new Post();
    
    public string UserId { get; set; } = String.Empty;
    public User User { get; set; } = new User();
}