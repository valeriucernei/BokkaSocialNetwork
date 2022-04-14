using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.User;
using Common.Exceptions;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BL.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    
    public UsersService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task<UserLoginResponseDto> Login(UserLoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            throw new LoginException("Wrong username or password.");
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Sid, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email!),
            new (ClaimTypes.Name, user.UserName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims);

        return new UserLoginResponseDto
        {
            token_type = "Bearer",
            access_token = new JwtSecurityTokenHandler().WriteToken(token),
            expires_at = token.ValidTo
        };
    }

    public async Task<UserRegisterResponseDto> Register(UserRegisterDto model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            throw new RegisterFormException("This username is already taken.");
        
        if (model.Username is {Length: < 5})
            throw new RegisterFormException("Username is too short.");

        if (model.FirstName is {Length: < 3})
            throw new RegisterFormException("First Name is too short.");

        User user = _mapper.Map<User>(model);
        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new RegisterFormException("Something went wrong. Please, try again.");

        return new UserRegisterResponseDto
        {
            Message = "Successfully registered! You can proceed to Login page."
        };
    }

    public async Task<UserRegisterResponseDto> RegisterAdmin(UserRegisterDto model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            throw new RegisterFormException("This username is already taken.");

        User user = _mapper.Map<User>(model);
        
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
            throw new RegisterFormException("Something went wrong. Please, try again.");

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new Role(UserRoles.Admin));
        
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new Role(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        
        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        
        return new UserRegisterResponseDto
        {
            Message = "Successfully registered! You can proceed to Login page."
        };
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public async Task<User> GetUserByClaims(ClaimsPrincipal user)
    {
        return await _userManager.FindByIdAsync(user.FindFirstValue(ClaimTypes.Sid));
    }
}