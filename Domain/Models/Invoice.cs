using Common.Enums;
using Domain.Models.Auth;

namespace Domain.Models;

public sealed class Invoice : BaseEntity
{
    public int Amount { get; set; } = 0;
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
    
    public string UserId { get; set; } = String.Empty;
    public User User { get; set; } = new User();
}