namespace Common.Dtos.Subscription;

public class SubscriptionDto
{
    public Guid Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public Guid UserId { get; set; }
}