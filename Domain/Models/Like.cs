using Domain.Models.Auth;

namespace Domain.Models;

public sealed class Like : BaseEntity
{
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    
    public Guid PostId { get; set; } = Guid.Empty;
    public Post Post { get; set; } = new Post();
    
    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = new User();

    public override string ToString()
    {
        return $"*** Like Info ***" +
               $"\n- Id: {Id}" +
               $"\n- CreatedDateTime: {CreatedDateTime}" +
               $"\n- PostId: {PostId}" +
               $"\n- UserId: {UserId}";
    }
}