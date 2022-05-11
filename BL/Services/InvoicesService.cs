using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Invoice;
using Common.Exceptions;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class InvoicesService : IInvoicesService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;
    private readonly IInvoicesRepository _invoicesRepository;
    
    public InvoicesService(
        IRepository repository, 
        IMapper mapper, 
        IUsersService usersService,
        IInvoicesRepository invoicesRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _usersService = usersService;
        _invoicesRepository = invoicesRepository;
    }

    public async Task<InvoiceDto?> GetInvoiceById(Guid id)
    {
        var invoice = await _repository.GetById<Invoice>(id);

        if (invoice is null)
            throw new NotFoundException("There is no invoice with such Id.");
        
        var result = _mapper.Map<InvoiceDto>(invoice);

        return result;
    }

    public async Task<List<InvoiceDto>> GetPersonalInvoices(ClaimsPrincipal userClaims)
    {
        var user = await _usersService.GetUserByClaims(userClaims);

        var invoices = await _invoicesRepository.GetInvoicesByUserId(user.Id);
        
        var result = _mapper.Map<List<InvoiceDto>>(invoices);

        return result;
    }
    
    public async Task<InvoiceDto> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims)
    {
        var invoice = _mapper.Map<Invoice>(invoiceForUpdateDto);

        invoice.User = await _usersService.GetUserByClaims(userClaims);
        
        await _repository.Add(invoice);
        
        await _repository.SaveChangesAsync();
        
        var result = _mapper.Map<InvoiceDto>(invoice);

        return result;
    }
    
    public async Task<InvoiceDto> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims)
    {
        var invoice = await _repository.GetById<Invoice>(id);

        if (invoice is null)
            throw new NotFoundException("There is no invoice with such Id.");

        var user = await _usersService.GetUserByClaims(userClaims);

        if (user.Id != invoice.UserId)
            throw new NotAllowedException("You are not allowed to edit this invoice.");

        _mapper.Map(invoiceForUpdateDto, invoice);
        
        await _repository.SaveChangesAsync();
        
        var result = _mapper.Map<InvoiceDto>(invoice);

        return result;
    }
}