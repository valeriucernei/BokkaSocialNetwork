using System.Security.Claims;
using Common.Dtos.Like;

namespace BL.Interfaces;

public interface ILikesService
{
    Task<List<LikeListOfPostDto>> GetLikesOfPost(Guid postId);
    Task<List<LikeListOfUserDto>> GetLikesOfUser(Guid userId);
    Task<LikeResponseDto> LikeAction(LikeCreateDto likeCreateDto, ClaimsPrincipal userClaims);
}