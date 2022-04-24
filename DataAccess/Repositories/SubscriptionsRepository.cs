using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly Context _context;

    public SubscriptionsRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<List<Subscription>> GetSubscriptionsByUserId(Guid id)
    {
        return await _context.Subscriptions
            .Where(p => p.UserId == id)
            .Include(p => p.User)
            .ToListAsync();
    }
}