using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Photo;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class PhotosService : IPhotosService
{
    private readonly IRepository _repository;
    private readonly IPhotosRepository _photosRepository;
    private readonly IMapper _mapper;
    
    public PhotosService(IRepository repository, IPhotosRepository photosRepository, IMapper mapper)
    {
        _repository = repository;
        _photosRepository = photosRepository;
        _mapper = mapper;
    }
    
    public async Task<List<PhotoDto>> GetPhotosByPostId(Guid postId)
    {
        var photos = await _photosRepository.GetPhotosByPhotoId(postId);

        return _mapper.Map<List<PhotoDto>>(photos);
    }
    
    public async Task<Response> UploadPhoto(PhotoUploadDto photoUploadDto, string directoryPath)
    {
        var photo = _mapper.Map<Photo>(photoUploadDto);
        var post = _repository.GetById<Post>(photoUploadDto.PostId).Result;

        photo.Post = post;
        _repository.Add(photo);

        var task1 = _repository.SaveChangesAsync();
        
        var bytes = Convert.FromBase64String(photoUploadDto.Base64);

        var filePath = Path.Combine(directoryPath, photo.Id.ToString());

        var task2 = File.WriteAllBytesAsync($"{filePath}.{photoUploadDto.Extension}", bytes);

        await Task.WhenAll(task1, task2);

        return new Response 
        {
            Message = "Photos uploaded successfully."
        };
    }
}