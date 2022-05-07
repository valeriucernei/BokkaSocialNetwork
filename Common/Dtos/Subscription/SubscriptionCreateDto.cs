using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Subscription;

public class SubscriptionCreateDto
{
    [Required]
    public Guid UserId { get; set; }
}