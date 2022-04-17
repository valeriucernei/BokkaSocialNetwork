using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Like;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models;

namespace BL.Services;

public class LikesService : ILikesService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly ILikesRepository _likesRepository;
    private readonly IUsersService _usersService;

    public LikesService(IMapper mapper, 
        IRepository repository, 
        ILikesRepository likesRepository, 
        IUsersService usersService)
    {
        _mapper = mapper;
        _repository = repository;
        _likesRepository = likesRepository;
        _usersService = usersService;
    }

    public async Task<List<LikeListOfPostDto>> GetLikesOfPost(Guid postId)
    {
        var post = await _repository.GetByIdWithInclude<Post>(postId, p => p.Likes);

        return _mapper.Map<List<LikeListOfPostDto>>(post.Likes.ToList());
    }
    
    public async Task<List<LikeListOfUserDto>> GetLikesOfUser(Guid userId)
    {
        var likes = await _likesRepository.GetLikesOfUser(userId);

        return _mapper.Map<List<LikeListOfUserDto>>(likes);
    }

    public async Task<Response> LikeAction(LikeCreateDto likeCreateDto, ClaimsPrincipal userClaims)
    {
        var user = await _usersService.GetUserByClaims(userClaims);
        var like = await _likesRepository.GetLikeByPostAndUser(likeCreateDto.PostId, user.Id);
        
        if (like is null)
            return await Like(likeCreateDto, userClaims);

        return await Dislike(like);
    }

    private async Task<Response> Like(LikeCreateDto likeCreateDto, ClaimsPrincipal userClaims)
    {
        var like = _mapper.Map<Like>(likeCreateDto);
        
        like.Post = await _repository.GetById<Post>(likeCreateDto.PostId);
        like.User = await _usersService.GetUserByClaims(userClaims);
        
        _repository.Add(like);
        await _repository.SaveChangesAsync();
        
        return new Response
        {
            Message = "Post successfully liked!"
        };
    }

    private async Task<Response> Dislike(Like like)
    {
        await _repository.Delete<Like>(like.Id);
        await _repository.SaveChangesAsync();

        return new Response
        {
            Message = "Post successfully disliked!"
        };
    }

}