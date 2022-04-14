using BL.Interfaces;
using Common.Dtos.Like;
using Common.Models;
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
    public async Task<List<LikeListOfPostDto>> GetLikesOfPost(Guid postId)
    {
        return await _likesService.GetLikesOfPost(postId);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId:guid}")]
    public async Task<List<LikeListOfUserDto>> GetLikesOfUser(Guid userId)
    {
        return await _likesService.GetLikesOfUser(userId);
    }

    [HttpPost("like-action")]
    public async Task<Response> LikeAction([FromBody] LikeCreateDto likeCreateDto)
    {
        return await _likesService.LikeAction(likeCreateDto, User);
    }
}