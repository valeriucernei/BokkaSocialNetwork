namespace Common.Dtos.Invoice;

public class CreateCheckoutSessionResponseDto
{
    public string SessionId { get; set; } = String.Empty;
    
    public string PublicKey { get; set; } = String.Empty;
}