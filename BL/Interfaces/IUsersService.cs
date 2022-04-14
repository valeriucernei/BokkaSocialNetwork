using System.Security.Claims;
using Common.Dtos.User;
using Domain.Models.Auth;

namespace BL.Interfaces;

public interface IUsersService
{
    Task<UserLoginResponseDto> Login(UserLoginDto model);
    Task<UserRegisterResponseDto> Register(UserRegisterDto model);
    Task<UserRegisterResponseDto> RegisterAdmin(UserRegisterDto model);
    Task<User> GetUserByClaims(ClaimsPrincipal user);
}