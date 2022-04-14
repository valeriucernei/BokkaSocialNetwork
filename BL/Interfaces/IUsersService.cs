using System.Security.Claims;
using Common.Dtos.User;
using Common.Models;
using Domain.Models.Auth;

namespace BL.Interfaces;

public interface IUsersService
{
    Task<UserDto> GetUser(ClaimsPrincipal userClaims);
    Task<UserDto> GetUser(Guid id);
    Task<UserLoginResponseDto> Login(UserLoginDto model);
    Task<UserRegisterResponseDto> Register(UserRegisterDto model);
    Task<UserRegisterResponseDto> RegisterAdmin(UserRegisterDto model);
    Task<User> GetUserByClaims(ClaimsPrincipal user);
    Task<UserRegisterDto> UpdateUser(UserRegisterDto model, ClaimsPrincipal userClaims);
    Task LogOut();
    Task<Response> DeleteUser(ClaimsPrincipal userClaims);
}