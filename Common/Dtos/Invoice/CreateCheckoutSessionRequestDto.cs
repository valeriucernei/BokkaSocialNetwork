using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Invoice;

public class CreateCheckoutSessionRequestDto
{
    [Required]
    public string PriceId { get; set; } = String.Empty;
    
    [Required]
    public int Price { get; set; }
    
    [Required]
    public string SuccessUrl { get; set; } = String.Empty;
    
    [Required]
    public string FailureUrl { get; set; } = String.Empty;

    [Required]
    public string ClientReferenceId { get; set; } = String.Empty;
    
    [Required] 
    public string CustomerEmail { get; set; } = String.Empty;
}