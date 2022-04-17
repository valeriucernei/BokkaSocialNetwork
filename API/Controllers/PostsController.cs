using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostsService _postsService;
    private readonly IUsersService _usersService;
    private readonly UserManager<User> _userManager;

    public PostsController(IPostsService postsService, IUsersService usersService, UserManager<User> userManager)
    {
        _postsService = postsService;
        _usersService = usersService;
        _userManager = userManager;
    }
    
    [AllowAnonymous]
    [HttpGet("post/{id:guid}")]
    public async Task<IActionResult> GetPost(Guid id)
    {
        var result =  await _postsService.GetPost(id);
    
        if (result is null)
            return NotFound("There is no post with such Id.");
    
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetUsersPosts(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
            return NotFound("There is no user with such Id.");
        
        var result = await _postsService.GetUsersPosts(userId);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("search")]
    public async Task<IActionResult> GetPagedPosts([FromBody] PagedRequest pagedRequest)
    {
        var result = await _postsService.GetPagedPosts(pagedRequest);

        return Ok(result);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost(PostForUpdateDto postForUpdateDto)
    {
        var postDto = await _postsService.CreatePost(postForUpdateDto, User);

        return CreatedAtAction(nameof(GetPost), new { id = postDto.Id }, postDto);
    }
    
    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdatePost(Guid id, PostForUpdateDto postForUpdateDto)
    {
        var post =  await _postsService.GetPost(id);
    
        if (post is null)
            return NotFound("There is no post with such Id.");

        var user = _usersService.GetUserByClaims(User).Result;

        if (user.Id != post.UserId)
            return BadRequest("You are not allowed to edit this post.");
        
        var postDto = await _postsService.UpdatePost(id, postForUpdateDto, User);
        
        return CreatedAtAction(nameof(GetPost), new { id = postDto.Id }, postDto);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var post =  await _postsService.GetPost(id);
    
        if (post is null)
            return NotFound("There is no post with such Id.");

        var user = _usersService.GetUserByClaims(User).Result;

        if (user.Id != post.UserId)
            return BadRequest("You are not allowed to delete this post.");
        
        var result = await _postsService.DeletePost(id, User);

        return Ok(result);
    }
}