using AutoMapper;
using BL.Models.Dtos.User;
using Domain.Models.Auth;

namespace BL.Profiles;

public class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<UserRegisterDto, User>();
    }
}