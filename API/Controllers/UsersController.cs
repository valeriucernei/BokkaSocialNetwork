using BL.Interfaces;
using Common.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController: ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<UserLoginResponseDto> Login([FromBody] UserLoginDto model)
    {
        return await _usersService.Login(model);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<UserRegisterResponseDto> Register([FromBody] UserRegisterDto model)
    {
        return await _usersService.Register(model);
    }
    
    [HttpPost]
    [Route("register-admin")]
    public async Task<UserRegisterResponseDto> RegisterAdmin([FromBody] UserRegisterDto model)
    {
        return await _usersService.RegisterAdmin(model);
    }
}