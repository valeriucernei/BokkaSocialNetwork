using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("posts/[controller]")]
public class PostsController : BaseController
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpPost("paginated-search")]
    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
        return await _postService.GetPagedPosts(pagedRequest);
    }
    
    [HttpGet("{id}")]
    public async Task<PostDto> GetPost(Guid id)
    {
        return await _postService.GetPost(id);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost(PostForUpdateDto postForUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var postDto = await _postService.CreatePost(postForUpdateDto);

        return CreatedAtAction(nameof(GetPost), new { id = postDto.Id }, postDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id, PostForUpdateDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _postService.UpdatePost(id, postDto);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task DeletePost(Guid id)
    {
        await _postService.DeletePost(id);
    }
}