using BL.Interfaces;
using Common.Dtos.Photo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IPhotosService _photosService;
    private readonly IPostsService _postsService;
    private readonly IUsersService _usersService;

    public PhotosController(
        IWebHostEnvironment webHostEnvironment, 
        IPhotosService photosService,
        IPostsService postsService, 
        IUsersService usersService)
    {
        _webHostEnvironment = webHostEnvironment;
        _photosService = photosService;
        _postsService = postsService;
        _usersService = usersService;
    }

    [AllowAnonymous]
    [HttpGet("post/{postId:guid}")]
    public async Task<IActionResult> GetPhotosByPostId(Guid postId)
    {
        var post =  await _postsService.GetPost(postId);
    
        if (post is null)
            return NotFound("There is no post with such Id.");
        
        var result = await _photosService.GetPhotosByPostId(postId);

        return Ok(result);
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadPhotos(PhotoUploadDto photoUploadDto)
    {
        var post =  await _postsService.GetPost(photoUploadDto.PostId);
    
        if (post is null)
            return NotFound("There is no post with such Id.");
        
        var user = await _usersService.GetUserByClaims(User);
        
        if (user.Id != post.UserId)
            return BadRequest("You are not allowed to edit this post.");
        
        var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/photos");
        
        var result = await _photosService.UploadPhoto(photoUploadDto, directoryPath);
        
        return Ok(result);
    }
}