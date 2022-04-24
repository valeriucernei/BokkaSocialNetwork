using Domain.Models;

namespace DataAccess.Interfaces;

public interface IPhotosRepository
{
    Task<List<Photo>> GetPhotosByPhotoId(Guid id);
}