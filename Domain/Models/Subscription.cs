namespace Domain.Models;

public class Subscription : EntityBase
{
    public DateTime StartDateTime { get; set; } = DateTime.Now;
    public DateTime EndDateTime { get; set; } = DateTime.Now;

    public Guid UserId { get; set; }
    public User User { get; set; } = new User();
}