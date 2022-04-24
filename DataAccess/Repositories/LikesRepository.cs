using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class LikesRepository : ILikesRepository
{
    private readonly Context _context;

    public LikesRepository(Context context)
    {
        _context = context;
    }

    public async Task<Like?> GetLikeByPostAndUser(Guid postId, Guid userId)
    {
        return await _context.Likes
            .Where(l => l.UserId == userId && l.PostId == postId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Like>> GetLikesOfUser(Guid userId)
    {
        return await _context.Likes
            .Where(l => l.UserId == userId)
            .ToListAsync();
    }
    
    
}