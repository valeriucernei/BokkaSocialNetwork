using BL.Interfaces;
using Common.Dtos.Like;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly ILikesService _likesService;
    private readonly IPostsService _postsService;
    private readonly IUsersService _usersService;

    public LikesController(ILikesService likesService, IPostsService postsService, IUsersService usersService)
    {
        _likesService = likesService;
        _postsService = postsService;
        _usersService = usersService;
    }
    
    [AllowAnonymous]
    [HttpGet("post/{postId:guid}")]
    public async Task<IActionResult> GetLikesOfPost(Guid postId)
    {
        var post =  await _postsService.GetPost(postId);
    
        if (post is null)
            return NotFound("There is no post with such Id.");
        
        var result = await _likesService.GetLikesOfPost(postId);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetLikesOfUser(Guid userId)
    {
        var user = await _usersService.GetUser(userId);

        if (user is null)
            return NotFound("There is no user with such Id.");
        
        var result = await _likesService.GetLikesOfUser(userId);

        return Ok(result);
    }

    [HttpPost("like-action")]
    public async Task<IActionResult> LikeAction([FromBody] LikeCreateDto likeCreateDto)
    {
        var result = await _likesService.LikeAction(likeCreateDto, User);

        return Ok(result);
    }
}