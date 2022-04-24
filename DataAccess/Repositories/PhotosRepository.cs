using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class PhotosRepository : IPhotosRepository
{
    private readonly Context _context;

    public PhotosRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<List<Photo>> GetPhotosByPhotoId(Guid id)
    {
        return await _context.Photos
            .Where(p => p.PostId == id)
            .ToListAsync();
    }
}