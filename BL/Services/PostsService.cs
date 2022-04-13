using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class PostsService : IPostsService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    
    public PostsService(IMapper mapper, IRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
         return await _repository.GetPagedData<Post, PostListDto>(pagedRequest);
    }
}