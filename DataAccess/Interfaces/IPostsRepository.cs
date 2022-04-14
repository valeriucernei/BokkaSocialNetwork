using Domain.Models;

namespace DataAccess.Interfaces;

public interface IPostsRepository
{
    Task<List<Post>> GetPostsByUserId(Guid id);
}