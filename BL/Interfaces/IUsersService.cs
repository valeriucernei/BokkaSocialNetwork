using BL.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace BL.Interfaces;

public interface IUsersService
{
    public Task<UserLoginResponseDto> Login([FromBody] UserLoginDto model);
    public Task<UserRegisterResponseDto> Register([FromBody] UserRegisterDto model);
    public Task<UserRegisterResponseDto> RegisterAdmin([FromBody] UserRegisterDto model);
}