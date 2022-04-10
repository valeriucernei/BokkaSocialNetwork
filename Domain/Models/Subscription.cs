namespace Domain.Models;

public sealed class Subscription : BaseEntity
{
    public DateTime StartDateTime { get; set; } = DateTime.Now;
    public DateTime EndDateTime { get; set; } = DateTime.Now;

    public Guid UserId { get; set; }
    public User User { get; set; } = new User();
}