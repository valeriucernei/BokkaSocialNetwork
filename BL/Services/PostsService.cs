using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Post;
using Common.Exceptions;
using Common.Models;
using Common.Models.PagedRequest;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class PostsService : IPostsService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly IUsersService _usersService;
    private readonly IPostsRepository _postsRepository;

    public PostsService(
        IMapper mapper, 
        IRepository repository, 
        IUsersService usersService, 
        IPostsRepository postsRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _usersService = usersService;
        _postsRepository = postsRepository;
    }
    
    public async Task<PostDto?> GetPost(Guid id)
    {
        var post = await _repository.GetById<Post>(id);

        if (post is null)
            throw new NotFoundException("There is no post with such Id.");
        
        var result = _mapper.Map<PostDto>(post);

        return result;
    }

    public async Task<List<PostListDto>> GetUsersPosts(Guid userId)
    {
        var posts = await _postsRepository.GetPostsByUserId(userId);
        
        var result = _mapper.Map<List<PostListDto>>(posts);

        return result;
    }
    
    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
        var result = await _repository.GetPagedData<Post, PostListDto>(pagedRequest);
        
        return result;
    }
    
    public async Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto, ClaimsPrincipal userClaims)
    {
        var post = _mapper.Map<Post>(postForUpdateDto);

        post.User = await _usersService.GetUserByClaims(userClaims);
        
        await _repository.Add(post);
        
        await _repository.SaveChangesAsync();

        return _mapper.Map<PostDto>(post);
    }
    
    public async Task<PostDto> UpdatePost(Guid id, PostForUpdateDto postForUpdateDto, ClaimsPrincipal userClaims)
    {
        var post = await _repository.GetById<Post>(id);

        if (post is null)
            throw new NotFoundException("There is no post with such Id.");
        
        var user = await _usersService.GetUserByClaims(userClaims);

        if (user.Id != post.UserId)
            throw new NotAllowedException("You are not allowed to edit this post.");

        _mapper.Map(postForUpdateDto, post);
        
        await _repository.SaveChangesAsync();
        
        var result = _mapper.Map<PostDto>(post);

        return result;
    }
    
    public async Task<Response> DeletePost(Guid id, ClaimsPrincipal userClaims)
    {
        var post = await _repository.GetById<Post>(id);

        if (post is null)
            throw new NotFoundException("There is no post with such Id.");
        
        var user = await _usersService.GetUserByClaims(userClaims);

        if (user.Id != post.UserId)
            throw new NotAllowedException("You are not allowed to delete this post.");
        
        await _repository.Delete<Post>(id);
        await _repository.SaveChangesAsync();

        return new Response()
        {
            Message = "You have successfully deleted this post."
        };
    }

    public async Task<List<PostListDto>> GetTopPosts()
    {
        var posts = await _postsRepository.GetTopPosts();
        return _mapper.Map<List<PostListDto>>(posts);
    }

}