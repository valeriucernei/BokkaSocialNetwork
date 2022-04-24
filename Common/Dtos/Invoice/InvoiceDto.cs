using Common.Enums;

namespace Common.Dtos.Invoice;

public class InvoiceDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public int Amount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public InvoiceStatus Status { get; set; }
    public Guid UserId { get; set; } = Guid.Empty;
}