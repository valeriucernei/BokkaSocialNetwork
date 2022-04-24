using System.Security.Claims;
using Common.Dtos.Invoice;

namespace BL.Interfaces;

public interface IInvoicesService
{
    Task<InvoiceDto?> GetInvoiceById(Guid id);
    Task<List<InvoiceDto>> GetPersonalInvoices(ClaimsPrincipal userClaims);
    Task<InvoiceDto> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims);
    Task<InvoiceDto> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims);
}