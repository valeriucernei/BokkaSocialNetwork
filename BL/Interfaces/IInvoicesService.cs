using System.Security.Claims;
using Common.Dtos.Invoice;
using Stripe;

namespace BL.Interfaces;

public interface IInvoicesService
{
    Task<InvoiceDto?> GetInvoiceById(Guid id);
    Task<List<InvoiceDto>> GetPersonalInvoices(ClaimsPrincipal userClaims);
    // Task<InvoiceDto> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims);
    // Task<InvoiceDto> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims);
    Task<CreateCheckoutSessionResponseDto> CreateCheckoutSession(CreateCheckoutSessionRequestDto req, ClaimsPrincipal userClaims);
    Task<object> StripeWebhook(Event stripeEvent);
}