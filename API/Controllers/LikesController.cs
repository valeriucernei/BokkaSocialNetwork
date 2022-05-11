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

    public LikesController(ILikesService likesService)
    {
        _likesService = likesService;
    }
    
    [AllowAnonymous]
    [HttpGet("post/{postId:guid}")]
    public async Task<IActionResult> GetLikesOfPost(Guid postId)
    {
        var result = await _likesService.GetLikesOfPost(postId);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetLikesOfUser(Guid userId)
    {
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