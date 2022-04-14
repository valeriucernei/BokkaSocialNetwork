using System.Security.Claims;
using Common.Dtos.Post;
using Common.Models.PagedRequest;

namespace BL.Interfaces;

public interface IPostsService
{
    public Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest);
    public Task<PostDto> GetPost(Guid id);
    public Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto, ClaimsPrincipal user);
}