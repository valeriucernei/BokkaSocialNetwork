using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class InvoicesRepository : IInvoicesRepository
{
    private readonly Context _context;

    public InvoicesRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<List<Invoice>> GetInvoicesByUserId(Guid id)
    {
        return await _context.Invoices.Where(p => p.UserId == id).ToListAsync();
    }
}