using Domain.Models.Auth;

namespace Domain.Models;

public sealed class Subscription : BaseEntity
{
    public DateTime StartDateTime { get; set; } = DateTime.Now;
    public DateTime EndDateTime { get; set; } = DateTime.Now;
    
    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = new User();
}