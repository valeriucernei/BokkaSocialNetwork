using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.Dtos.Invoice;

public class InvoiceForUpdateDto
{
    [Required]
    public int Amount { get; set; } = 100;

    [Required] 
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
}