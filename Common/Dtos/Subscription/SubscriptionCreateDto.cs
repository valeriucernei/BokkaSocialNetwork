using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Subscription;

public class SubscriptionCreateDto
{
    [Required] public string Email { get; set; } = String.Empty;

    [Required]
    public DateTime StartDateTime { get; set; }
    
    [Required]
    public DateTime EndDateTime { get; set; }
}