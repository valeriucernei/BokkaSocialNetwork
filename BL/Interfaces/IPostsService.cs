using Common.Dtos.Post;
using Common.Models.PagedRequest;

namespace BL.Interfaces;

public interface IPostsService
{
    public Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest);
}