using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Post;
using Common.Exceptions;
using Common.Models.PagedRequest;
using DataAccess.Interfaces;
using Domain.Models;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace BL.Services;

public class PostsService : IPostsService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly UserManager<User> _userManager;

    public PostsService(IMapper mapper, IRepository repository, UserManager<User> userManager)
    {
        _mapper = mapper;
        _repository = repository;
        _userManager = userManager;
    }
    
    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
         return await _repository.GetPagedData<Post, PostListDto>(pagedRequest);
    }

    public async Task<PostDto> GetPost(Guid id)
    {
        if (!_repository.ExistsById<Post>(id).Result)
            throw new NotFoundException("There is no post with such Id.");
        
        var post = await _repository.GetById<Post>(id);

        return _mapper.Map<PostDto>(post);
    }
    
    public async Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto, ClaimsPrincipal user)
    {
        var post = _mapper.Map<Post>(postForUpdateDto);

        //post.User = await _userManager.FindByIdAsync("AABCB95C-3A83-4B17-047D-08DA1D55CCE4");
        post.User = await _userManager.FindByIdAsync(user.FindFirstValue(ClaimTypes.Sid));

        Console.WriteLine(post.ToString());
        
        _repository.Add(post);
        
        await _repository.SaveChangesAsync();

        return _mapper.Map<PostDto>(post);
    }
}