using BL.Interfaces;
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

    public PhotosController(
        IWebHostEnvironment webHostEnvironment, 
        IPhotosService photosService)
    {
        _webHostEnvironment = webHostEnvironment;
        _photosService = photosService;
    }

    [AllowAnonymous]
    [HttpGet("post/{postId:guid}")]
    public async Task<IActionResult> GetPhotosByPostId(Guid postId)
    {
        var result = await _photosService.GetPhotosByPostId(postId);

        return Ok(result);
    }

    [HttpPost("upload")]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Upload()
    {
        var formCollection = await Request.ReadFormAsync();
        var file = formCollection.Files.First();

        formCollection.TryGetValue("postId", out var value);
        Guid postId = Guid.Parse(value.ToString());

        var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/photos");

        var result = await _photosService.Upload(file, postId, directoryPath, User);

        return Ok(result);
    }
    
}