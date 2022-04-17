using System.Security.Claims;
using Common.Dtos.Post;
using Common.Models;
using Common.Models.PagedRequest;
using Domain.Models;
using Domain.Models.Auth;

namespace BL.Interfaces;

public interface IPostsService
{
    Task<PostDto?> GetPost(Guid id);
    //Task<Post> GetPost(Guid id);
    Task<List<PostListDto>> GetUsersPosts(Guid userId);
    Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest);
    Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto, ClaimsPrincipal user);
    Task<PostDto> UpdatePost(Guid id, PostForUpdateDto postForUpdateDto, ClaimsPrincipal user);
    Task<Response> DeletePost(Guid id, ClaimsPrincipal userClaims);
}