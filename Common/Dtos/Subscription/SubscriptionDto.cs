namespace Common.Dtos.Subscription;

public class SubscriptionDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public Guid UserId { get; set; } = Guid.Empty;
}