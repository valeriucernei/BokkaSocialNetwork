using BL.Interfaces;
using Common.Dtos.User;
using Common.Exceptions;
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
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await _usersService.GetUser(id);

        if (result is null)
            return NotFound("There is no user with such Id.");

        return Ok(result);
    }
    
    [AllowAnonymous]
    [ApiExceptionFilter]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        var result = await _usersService.Login(model);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [ApiExceptionFilter]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
    {
        var result = await _usersService.Register(model);
        
        return Ok(result);
    }
    
    [ApiExceptionFilter]
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDto model)
    {
        var result = await _usersService.RegisterAdmin(model);

        return Ok(result);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _usersService.LogOut();
        return Ok(new {});
    }
    
    [ApiExceptionFilter]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UserRegisterDto model)
    {
        var result = await _usersService.UpdateUser(model, User);

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        var result = await _usersService.DeleteUser(User);

        return Ok(result);
    }
}