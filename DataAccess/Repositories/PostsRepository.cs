using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PostsRepository : IPostsRepository
{
    private readonly Context _context;

    public PostsRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<List<Post>> GetPostsByUserId(Guid id)
    {
        var posts = await _context.Posts.Where(p => p.UserId == id).Include(p => p.User).ToListAsync();
        return posts;
    }
}