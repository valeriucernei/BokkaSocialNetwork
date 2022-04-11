using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    
    [HttpGet("post/{id}")]
    public async Task<PostDto> GetPost(string id)
    {
        return await _postService.GetPost(id);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> CreatePost(PostForUpdateDto postForUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var postDto = await _postService.CreatePost(postForUpdateDto);

        return CreatedAtAction(nameof(GetPost), new { id = postDto.Id }, postDto);
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePost(string id, PostForUpdateDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _postService.UpdatePost(id, postDto);
        return Ok();
    }
    
    [HttpDelete("delete{id}")]
    public async Task DeletePost(string id)
    {
        await _postService.DeletePost(id);
    }
}