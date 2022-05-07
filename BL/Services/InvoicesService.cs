using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Invoice;
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
        var result = await _repository.GetById<Invoice>(id);
        return _mapper.Map<InvoiceDto>(result);
    }

    public async Task<List<InvoiceDto>> GetPersonalInvoices(ClaimsPrincipal userClaims)
    {
        var user = await _usersService.GetUserByClaims(userClaims);

        var result = await _invoicesRepository.GetInvoicesByUserId(user.Id);

        return _mapper.Map<List<InvoiceDto>>(result);
    }
    
    public async Task<InvoiceDto> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims)
    {
        var invoice = _mapper.Map<Invoice>(invoiceForUpdateDto);

        invoice.User = await _usersService.GetUserByClaims(userClaims);
        
        _repository.Add(invoice);
        
        await _repository.SaveChangesAsync();

        return _mapper.Map<InvoiceDto>(invoice);
    }
    
    public async Task<InvoiceDto> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims)
    {
        var invoice = await _repository.GetById<Invoice>(id);

        _mapper.Map(invoiceForUpdateDto, invoice);
        
        await _repository.SaveChangesAsync();
        
        return _mapper.Map<InvoiceDto>(invoice);
    }
}