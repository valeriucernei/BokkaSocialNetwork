using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Like;
using Common.Exceptions;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace BL.Services;

public class LikesService : ILikesService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly ILikesRepository _likesRepository;
    private readonly IUsersService _usersService;
    private readonly IPostsService _postsService;
    private readonly UserManager<User> _userManager;

    public LikesService(IMapper mapper, 
        IRepository repository, 
        ILikesRepository likesRepository, 
        IUsersService usersService, 
        IPostsService postsService,
        UserManager<User> userManager)
    {
        _mapper = mapper;
        _repository = repository;
        _likesRepository = likesRepository;
        _usersService = usersService;
        _postsService = postsService;
        _userManager = userManager;
    }

    public async Task<List<LikeListOfPostDto>> GetLikesOfPost(Guid postId)
    {
        if (!_repository.ExistsById<Post>(postId).Result)
            throw new NotFoundException("There is no post with such Id.");
        
        var post = await _repository.GetByIdWithInclude<Post>(postId, p => p.Likes);

        return _mapper.Map<List<LikeListOfPostDto>>(post.Likes.ToList());
    }
    
    public async Task<List<LikeListOfUserDto>> GetLikesOfUser(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
            throw new NotFoundException("There is no user with such Id.");

        var likes = _likesRepository.GetLikesOfUser(userId).Result;

        return _mapper.Map<List<LikeListOfUserDto>>(likes);
    }

    public async Task<Response> LikeAction(LikeCreateDto likeCreateDto, ClaimsPrincipal userClaims)
    {
        if (!_repository.ExistsById<Post>(likeCreateDto.PostId).Result)
            throw new NotFoundException("There is no post with such Id.");

        var user = _usersService.GetUserByClaims(userClaims).Result;
        var like = _likesRepository.GetLikeByPostAndUser(likeCreateDto.PostId, user.Id).Result;
        
        if (like is null)
            return await Like(likeCreateDto, userClaims);

        return await Dislike(like);
    }

    private async Task<Response> Like(LikeCreateDto likeCreateDto, ClaimsPrincipal userClaims)
    {
        try
        {
            var like = _mapper.Map<Like>(likeCreateDto);
            
            like.Post = _repository.GetById<Post>(likeCreateDto.PostId).Result;
            like.User = _usersService.GetUserByClaims(userClaims).Result;
            
            _repository.Add(like);
            await _repository.SaveChangesAsync();
            
            return new Response
            {
                Message = "Post successfully liked!"
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong.", ex);
        }
    }

    private async Task<Response> Dislike(Like like)
    {
        try
        {
            await _repository.Delete<Like>(like.Id);
            await _repository.SaveChangesAsync();

            return new Response
            {
                Message = "Post successfully disliked!"
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong.", ex);
        }
    }

}