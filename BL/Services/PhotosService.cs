using System.Security.Claims;
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
    private readonly IPostsService _postsService;
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;
    
    public PhotosService(
        IRepository repository, 
        IPhotosRepository photosRepository, 
        IPostsService postsService, 
        IUsersService usersService,
        IMapper mapper)
    {
        _repository = repository;
        _photosRepository = photosRepository;
        _postsService = postsService;
        _usersService = usersService;
        _mapper = mapper;
    }
    
    public async Task<List<PhotoDto>> GetPhotosByPostId(Guid postId)
    {
        var post =  await _postsService.GetPost(postId);

        if (post is null)
            throw new NotFoundException("There is no post with such Id.");
        
        var photos = await _photosRepository.GetPhotosByPostId(postId);

        return _mapper.Map<List<PhotoDto>>(photos);
    }

    public async Task<Response> Upload(IFormFile file, Guid postId, string directoryPath, ClaimsPrincipal userClaims)
    {
        if (file.Length == 0)
            throw new FormException("There are no photos to upload.");
        
        var post = await _repository.GetById<Post>(postId);

        if (post is null)
            throw new NotFoundException("There is no post with such Id.");
        
        var user = await _usersService.GetUserByClaims(userClaims);

        if (user.Id != post.UserId)
            throw new NotAllowedException("You are not allowed to upload photos to this post.");

        var photo = new Photo()
        {
            Post = post,
            Extension = "png"
        };

        await _repository.Add(photo);
        
        var task1 = _repository.SaveChangesAsync();

        var task2 = UploadPhoto(photo, file, directoryPath);
        
        await Task.WhenAll(task1, task2);

        return new Response 
        {
            Message = "Photos uploaded successfully."
        };

    }

    private async Task UploadPhoto(Photo photo, IFormFile file, string directoryPath)
    {
        var fullPath = Path.Combine(directoryPath, $"{photo.Id.ToString()}.png");

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
    }
    
}