using Common.Dtos.Post;
using Common.Models.PagedRequest;

namespace BL.Interfaces;

public interface IPostService
{
    Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest);

    Task<PostDto> GetPost(string id);

    Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto);

    Task UpdatePost(string id, PostForUpdateDto postDto);

    Task DeletePost(string id);
}