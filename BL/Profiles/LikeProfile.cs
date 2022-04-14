using AutoMapper;
using Common.Dtos.Like;
using Domain.Models;

namespace BL.Profiles;

public class LikeProfile : Profile
{
    public LikeProfile()
    {
        CreateMap<LikeCreateDto, Like>();
        CreateMap<Like, LikeListOfPostDto>();
        CreateMap<Like, LikeListOfUserDto>();
    }
}