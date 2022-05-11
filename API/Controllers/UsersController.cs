using BL.Interfaces;
using Common.Dtos.User;
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
    public async Task<IActionResult> GetUser()
    {
        var result = await _usersService.GetUser(User);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await _usersService.GetUser(id);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        var result = await _usersService.Login(model);

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
    {
        var result = await _usersService.Register(model);
        
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _usersService.LogOut();
        
        return Ok(new {});
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto model)
    {
        var user = await _usersService.UpdateUser(model, User);

        if (user.Errors.Any())
            return BadRequest(user.Errors);

        var result = await _usersService.GetUser(User);
        
        return Ok(result);
    }
    
    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UserUpdatePasswordDto model)
    {
        var user = await _usersService.UpdateUserPassword(model, User);

        if (user.Errors.Any())
            return BadRequest(user.Errors);

        return Ok(user);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        var result = await _usersService.DeleteUser(User);

        return Ok(result);
    }
}