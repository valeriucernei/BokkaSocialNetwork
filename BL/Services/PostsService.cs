using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Post;
using Common.Exceptions;
using Common.Models;
using Common.Models.PagedRequest;
using DataAccess.Interfaces;
using Domain.Models;
using Domain.Models.Auth;

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
    
        return _mapper.Map<PostDto>(post);
    }

    public async Task<List<PostListDto>> GetUsersPosts(Guid userId)
    {
        var posts = await _postsRepository.GetPostsByUserId(userId);

        return _mapper.Map<List<PostListDto>>(posts);
    }
    
    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
        if (pagedRequest.PageIndex < 0)
            throw new ArgumentException("Page index can't lower than 0.");

        if (pagedRequest.PageSize is < 0 or > 50)
            throw new ArgumentException("Page size out of bounds (0 and 50).");

        return await _repository.GetPagedData<Post, PostListDto>(pagedRequest);
    }
    
    public async Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto, ClaimsPrincipal userClaims)
    {
        var post = _mapper.Map<Post>(postForUpdateDto);

        post.User = _usersService.GetUserByClaims(userClaims).Result;
        
        _repository.Add(post);
        
        await _repository.SaveChangesAsync();

        return _mapper.Map<PostDto>(post);
    }
    
    public async Task<PostDto> UpdatePost(Guid id, PostForUpdateDto postForUpdateDto, ClaimsPrincipal userClaims)
    {
        var post = await _repository.GetById<Post>(id);

        _mapper.Map(postForUpdateDto, post);
        
        await _repository.SaveChangesAsync();
        
        return _mapper.Map<PostDto>(post);
    }
    
    public async Task<Response> DeletePost(Guid id, ClaimsPrincipal userClaims)
    {
        if (!_repository.ExistsById<Post>(id).Result)
            throw new NotFoundException("There is no post with such Id.");
        
        var post = await _repository.GetById<Post>(id);
        
        if (!IsUsersPost(_usersService.GetUserByClaims(userClaims).Result, post))
            throw new ForbiddenException("You are not allowed to delete this post.");
        
        await _repository.Delete<Post>(id);
        await _repository.SaveChangesAsync();

        return new Response()
        {
            Message = "You have successfully deleted this post."
        };
    }

    // public async Task DeleteAllUserPosts(ClaimsPrincipal userClaims)
    // {
    //     var user = _usersService.GetUserByClaims(userClaims).Result;
    //     var posts = GetUsersPosts(user.Id).Result;
    //
    //     foreach (var post in posts)
    //     {
    //         await DeletePost(post.Id, userClaims);
    //     }
    // }

    private static bool IsUsersPost(User user, Post post)
    {
        return user.Id == post.UserId;
    }
}