using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PostsRepository : IPostsRepository
{
    private readonly Context _context;
    private readonly ILikesRepository _likesRepository;

    public PostsRepository(Context context, ILikesRepository likesRepository)
    {
        _context = context;
        _likesRepository = likesRepository;
    }
    
    public async Task<List<Post>> GetPostsByUserId(Guid id)
    {
        return await _context.Posts
            .Where(p => p.UserId == id)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<Post> GetPost(Guid id)
    {
        return await _context.Posts
            .Where(p => p.Id == id)
            .Include(p => p.User)
            .Include(p => p.Likes)
            .FirstAsync();
    }

    public async Task<List<Post>> GetTopPosts()
    {
        return await _context.Posts
            .Include(p => p.Photos)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.Likes.Count)
            .Take(5)
            .ToListAsync();
    }

}