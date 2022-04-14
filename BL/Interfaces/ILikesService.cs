using System.Security.Claims;
using Common.Dtos.Like;
using Common.Models;

namespace BL.Interfaces;

public interface ILikesService
{
    Task<List<LikeListOfPostDto>> GetLikesOfPost(Guid postId);
    Task<List<LikeListOfUserDto>> GetLikesOfUser(Guid userId);
    Task<Response> LikeAction(LikeCreateDto likeCreateDto, ClaimsPrincipal userClaims);
}