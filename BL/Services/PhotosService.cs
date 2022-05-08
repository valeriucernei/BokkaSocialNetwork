using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Photo;
using Common.Exceptions;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;

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
        var photos = await _photosRepository.GetPhotosByPostId(postId);

        return _mapper.Map<List<PhotoDto>>(photos);
    }
    
    public async Task<Response> UploadPhoto(PhotoUploadDto photoUploadDto, string directoryPath)
    {
        var photo = _mapper.Map<Photo>(photoUploadDto);
        var post = await _repository.GetById<Post>(photoUploadDto.PostId);

        try
        {
            photo.Post = post;
            _repository.Add(photo);
            
            var task1 = _repository.SaveChangesAsync();

            try
            {
                var bytes = Convert.FromBase64String(photoUploadDto.Base64);

                var filePath = Path.Combine(directoryPath, photo.Id.ToString());

                var task2 = File.WriteAllBytesAsync($"{filePath}.{photoUploadDto.Extension}", bytes);

                await Task.WhenAll(task1, task2);
            }
            catch
            {
                throw new FormException("Photo couldn't be stored. Try again.");
            }
        }
        catch
        {
            throw new FormException("Photo couldn't be saved. Try again.");
        }

        return new Response 
        {
            Message = "Photos uploaded successfully."
        };
    }

    public async Task<Response> Upload(IFormFile file, Guid postId, string directoryPath)
    {
        var post = await _repository.GetById<Post>(postId);
        var photo = new Photo()
        {
            Post = post,
            Extension = "png"
        };

        try
        {
            _repository.Add(photo);

            if (file.Length > 0)
            {
                try
                {
                    var fullPath = Path.Combine(directoryPath, $"{photo.Id.ToString()}.png");

                    await using var stream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
                catch
                {
                    throw new FormException("Photo couldn't be stored. Try again.");
                }
            }
            else
            {
                throw new FormException("There are no files to upload.");
            }
            
            await _repository.SaveChangesAsync();
            return new Response
            {
                Message = "Photos uploaded successfully."
            };
        } 
        catch
        {
            throw new FormException("Photo couldn't be saved. Try again.");
        }
    }
    
}