using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostsService _postsService;

    public PostsController(IPostsService postsService)
    {
        _postsService = postsService;
    }
    
    [HttpPost]
    [Route("search")]
    public async Task<PaginatedResult<PostListDto>> GetPagedBooks([FromBody] PagedRequest pagedRequest)
    {
        var pagedBooksDto = await _postsService.GetPagedPosts(pagedRequest);
        return pagedBooksDto;  
    }
}