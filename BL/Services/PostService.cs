using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Post;
using Common.Models.PagedRequest;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class PostService : IPostService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public PostService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<PostListDto>> GetPagedPosts(PagedRequest pagedRequest)
    {
        var pagedPostsDto = await _repository.GetPagedData<Post, PostListDto>(pagedRequest);
        // checks...
        return pagedPostsDto;
    }

    public async Task<PostDto> GetPost(string id)
    {
        var post = await _repository.GetByIdWithInclude<Post>(id, post => post.User);
        // checks...
        return _mapper.Map<PostDto>(post);
    }

    public async Task<PostDto> CreatePost(PostForUpdateDto postForUpdateDto)
    {
        var post = _mapper.Map<Post>(postForUpdateDto);
        
        _repository.Add(post);
        await _repository.SaveChangesAsync();

        return _mapper.Map<PostDto>(post);
    }

    public async Task UpdatePost(string id, PostForUpdateDto postDto)
    {
        var post = await _repository.GetById<Post>(id);
        
        _mapper.Map(postDto, post);
        await _repository.SaveChangesAsync();
    }

    public async Task DeletePost(string id)
    {
        await _repository.Delete<Post>(id);
        await _repository.SaveChangesAsync();
    }
}