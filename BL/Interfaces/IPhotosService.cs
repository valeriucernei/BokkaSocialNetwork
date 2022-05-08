using Common.Dtos.Photo;
using Common.Models;
using Microsoft.AspNetCore.Http;

namespace BL.Interfaces;

public interface IPhotosService
{
    Task<List<PhotoDto>> GetPhotosByPostId(Guid postId);
    Task<Response> UploadPhoto(PhotoUploadDto photoUploadDto, string directoryPath);
    Task<Response> Upload(IFormFile file, Guid postId, string directoryPath);
}