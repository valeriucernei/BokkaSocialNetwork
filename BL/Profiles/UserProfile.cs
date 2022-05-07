using AutoMapper;
using Common.Dtos.User;
using Domain.Models.Auth;

namespace BL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegisterDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserRegisterDto>();
        CreateMap<User, UserUpdateDto>();
        CreateMap<User, UserDto>();
    }
}