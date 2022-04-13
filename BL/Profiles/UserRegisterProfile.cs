using AutoMapper;
using Common.Dtos.User;
using Domain.Models.Auth;

namespace BL.Profiles;

public class UserRegisterProfile : Profile
{
    public UserRegisterProfile()
    {
        CreateMap<UserRegisterDto, User>();
    }
}