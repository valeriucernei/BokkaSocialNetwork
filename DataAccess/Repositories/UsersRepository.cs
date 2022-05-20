using DataAccess.Interfaces;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly Context _context;

    public UsersRepository(Context context)
    {
        _context = context;
    }

    public async Task<bool> IsUserPaidUser(User user)
    {
        var currentDateTime = DateTime.UtcNow;
        var result = await _context.Subscriptions.Where(s => s.UserId == user.Id && s.EndDateTime > currentDateTime)
            .ToListAsync();

        return result.Count != 0;
    }
}