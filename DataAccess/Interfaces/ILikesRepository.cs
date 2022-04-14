using Domain.Models;

namespace DataAccess.Interfaces;

public interface ILikesRepository
{
    Task<Like?> GetLikeByPostAndUser(Guid postId, Guid userId);
    Task<List<Like>> GetLikesOfUser(Guid userId);
}