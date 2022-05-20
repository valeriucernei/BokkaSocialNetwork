using System.Configuration;
using BL.Interfaces;
using Common.Dtos.Invoice;
using Common.Exceptions;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoicesService _invoicesService;
    private readonly StripeSettings _stripeSettings;

    public InvoicesController(IInvoicesService invoicesService, IOptions<StripeSettings> stripeSettings)
    {
        _invoicesService = invoicesService;
        _stripeSettings = stripeSettings.Value;
    }
    
    [HttpGet("invoice/{id:guid}")]
    public async Task<IActionResult> GetInvoice(Guid id)
    {
        var result =  await _invoicesService.GetInvoiceById(id);
    
        return Ok(result);
    }
    
    [HttpGet("personal")]
    public async Task<IActionResult> GetPersonalInvoices()
    {
        var result = await _invoicesService.GetPersonalInvoices(User);
        
        return Ok(result);
    }
    
    // [HttpPost("create")]
    // public async Task<IActionResult> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto)
    // {
    //     var invoiceDto = await _invoicesService.CreateInvoice(invoiceForUpdateDto, User);
    //
    //     return CreatedAtAction(nameof(GetInvoice), new { id = invoiceDto.Id }, invoiceDto);
    // }
    //
    // [HttpPut("update/{id:guid}")]
    // public async Task<IActionResult> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto)
    // {
    //     var invoiceDto = await _invoicesService.UpdateInvoice(id, invoiceForUpdateDto, User);
    //     
    //     return CreatedAtAction(nameof(GetInvoice), new { id = invoiceDto.Id }, invoiceDto);
    // }
    
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequestDto req)
    {
        var result = await _invoicesService.CreateCheckoutSession(req, User);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<IActionResult> WebHook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(
            json,
            Request.Headers["Stripe-Signature"],
            _stripeSettings.WHSecret
        );

        var result = await _invoicesService.StripeWebhook(stripeEvent);

        return Ok(result);
    }
    
}