using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Post;
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
    
        return _mapper.Map<PostDto>(post);
    }

    public async Task<List<PostListDto>> GetUsersPosts(Guid userId)
    {
        var posts = await _postsRepository.GetPostsByUserId(userId);

        return _mapper.Map<List<PostListDto>>(posts);
    }
    
    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
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
        await _repository.Delete<Post>(id);
        await _repository.SaveChangesAsync();

        return new Response()
        {
            Message = "You have successfully deleted this post."
        };
    }
}