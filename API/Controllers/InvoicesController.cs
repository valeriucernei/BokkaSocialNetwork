using BL.Interfaces;
using Common.Dtos.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoicesService _invoicesService;
    private readonly IUsersService _usersService;

    public InvoicesController(IInvoicesService invoicesService, IUsersService userManager)
    {
        _invoicesService = invoicesService;
        _usersService = userManager;
    }
    
    [HttpGet("invoice/{id:guid}")]
    public async Task<IActionResult> GetInvoice(Guid id)
    {
        var result =  await _invoicesService.GetInvoiceById(id);
    
        if (result is null)
            return NotFound("There is no invoice with such Id.");
    
        return Ok(result);
    }
    
    [HttpGet("personal")]
    public async Task<IActionResult> GetPersonalInvoices()
    {
        var result = await _invoicesService.GetPersonalInvoices(User);
        return Ok(result);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto)
    {
        var invoiceDto = await _invoicesService.CreateInvoice(invoiceForUpdateDto, User);

        return CreatedAtAction(nameof(GetInvoice), new { id = invoiceDto.Id }, invoiceDto);
    }
    
    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto)
    {
        var invoice =  await _invoicesService.GetInvoiceById(id);
    
        if (invoice is null)
            return NotFound("There is no invoice with such Id.");

        var user = await _usersService.GetUserByClaims(User);

        if (user.Id != invoice.UserId)
            return BadRequest("You are not allowed to edit this invoice.");
        
        var invoiceDto = await _invoicesService.UpdateInvoice(id, invoiceForUpdateDto, User);
        
        return CreatedAtAction(nameof(GetInvoice), new { id = invoiceDto.Id }, invoiceDto);
    }
}