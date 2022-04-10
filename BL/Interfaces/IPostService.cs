using Common.Dtos.Post;
using Common.Models.PagedRequest;

namespace BL.Interfaces;

public interface IPostService
{
    Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest);

    Task<PostDto> GetPost(Guid id);

    Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto);

    Task UpdatePost(Guid id, PostForUpdateDto postDto);

    Task DeletePost(Guid id);
}