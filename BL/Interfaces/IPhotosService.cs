using Common.Dtos.Photo;
using Common.Models;

namespace BL.Interfaces;

public interface IPhotosService
{
    Task<List<PhotoDto>> GetPhotosByPostId(Guid postId);
    Task<Response> UploadPhoto(PhotoUploadDto photoUploadDto, string directoryPath);
}