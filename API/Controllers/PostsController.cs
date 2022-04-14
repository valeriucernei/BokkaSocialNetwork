using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models;
using Common.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostsService _postsService;

    public PostsController(IPostsService postsService)
    {
        _postsService = postsService;
    }

    [AllowAnonymous]
    [HttpGet("post/{id:guid}")]
    public async Task<PostDto> GetPost(Guid id)
    {
        return await _postsService.GetPost(id);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId:guid}")]
    public async Task<List<PostListDto>> GetUsersPosts(Guid userId)
    {
        return await _postsService.GetUsersPosts(userId);
    }
    
    [AllowAnonymous]
    [HttpPost("search")]
    public async Task<PaginatedResult<PostListDto>> GetPagedBooks([FromBody] PagedRequest pagedRequest)
    {
        return await _postsService.GetPagedPosts(pagedRequest);  
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
    
    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateBook(Guid id, PostForUpdateDto postForUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var postDto = await _postsService.UpdatePost(id, postForUpdateDto, User);
        
        return CreatedAtAction(nameof(GetPost), new { id = postDto.Id }, postDto);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<Response> DeleteBook(Guid id)
    {
        return await _postsService.DeletePost(id, User);
    }
}