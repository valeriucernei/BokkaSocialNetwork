using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostsController : ControllerBase
{
    private readonly IPostsService _postsService;

    public PostsController(IPostsService postsService)
    {
        _postsService = postsService;
    }
    
    [HttpPost("search")]
    public async Task<PaginatedResult<PostListDto>> GetPagedBooks([FromBody] PagedRequest pagedRequest)
    {
        return await _postsService.GetPagedPosts(pagedRequest);  
    }
    
    [HttpGet("post/{id}")]
    public async Task<PostDto> GetPost(Guid id)
    {
        return await _postsService.GetPost(id);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost(PostForUpdateDto postForUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var postDto = await _postsService.CreatePost(postForUpdateDto, User);

        return CreatedAtAction(nameof(GetPost), new { id = postDto.Id }, postDto);
    }
}