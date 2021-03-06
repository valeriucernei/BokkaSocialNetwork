using Domain.Models;

namespace DataAccess.Interfaces;

public interface IInvoicesRepository
{
    Task<List<Invoice>> GetInvoicesByUserId(Guid id);
    Task<Invoice?> GetInvoiceByStripeInvoiceId(string id);
}