using BL.Interfaces;
using Common.Dtos.User;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpGet("user")]
    public async Task<UserDto> GetUser()
    {
        return await _usersService.GetUser(User);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{id:guid}")]
    public async Task<UserDto> GetUser(Guid id)
    {
        return await _usersService.GetUser(id);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<UserLoginResponseDto> Login([FromBody] UserLoginDto model)
    {
        return await _usersService.Login(model);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<UserRegisterResponseDto> Register([FromBody] UserRegisterDto model)
    {
        return await _usersService.Register(model);
    }
    
    [HttpPost("register-admin")]
    public async Task<UserRegisterResponseDto> RegisterAdmin([FromBody] UserRegisterDto model)
    {
        return await _usersService.RegisterAdmin(model);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _usersService.LogOut();
        return Ok(new {});
    }
    
    [HttpPut("update")]
    public async Task<UserRegisterDto> Update([FromBody] UserRegisterDto model)
    {
        return await _usersService.UpdateUser(model, User);
    }

    [HttpDelete("delete")]
    public async Task<Response> Delete()
    {
        return await _usersService.DeleteUser(User);
    }
}