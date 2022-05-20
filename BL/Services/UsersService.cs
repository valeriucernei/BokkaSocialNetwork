using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.User;
using Common.Exceptions;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BL.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly IUsersRepository _usersRepository;

    public UsersService(
        UserManager<User> userManager,
        IConfiguration configuration,
        IMapper mapper,
        SignInManager<User> signInManager,
        IUsersRepository usersRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
        _signInManager = signInManager;
        _usersRepository = usersRepository;
    }

    public async Task<UserUpdateDto> GetUser(ClaimsPrincipal userClaims)
    {
        var user = await this.GetUserByClaims(userClaims);

        return _mapper.Map<UserUpdateDto>(user);
    }
    
    public async Task<UserDto?> GetUser(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
            throw new NotFoundException("There is no user with such Id.");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserLoginResponseDto> RefreshToken(ClaimsPrincipal userClaims)
    {
        var user = await GetUserByClaims(userClaims);

        await UpdateUserRole(user);
        
        var token = await GetToken(user);
        
        return new UserLoginResponseDto()
        {
            token_type = "Bearer",
            access_token = new JwtSecurityTokenHandler().WriteToken(token),
            expires_at = token.ValidTo
        };
    }
    
    public async Task<UserLoginResponseDto> Login(UserLoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
            throw new LoginException("Wrong credentials.");

        var token = await GetToken(user);

        return new UserLoginResponseDto()
        {
            token_type = "Bearer",
            access_token = new JwtSecurityTokenHandler().WriteToken(token),
            expires_at = token.ValidTo
        };
    }

    public async Task<UserRegisterResponseDto> Register(UserRegisterDto model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        
        if (userExists is not null)
            throw new EntryAlreadyExistsException("This UserName already exists.");
        
        var emailExists = await _userManager.FindByEmailAsync(model.Email);
        
        if (emailExists is not null)
            throw new EntryAlreadyExistsException("This Email already exists.");

        var user = _mapper.Map<User>(model);
        
        await _userManager.CreateAsync(user, model.Password);
        
        await _userManager.AddToRoleAsync(user, UserRoles.User);

        return new UserRegisterResponseDto
        {
            Message = "Successfully registered! You can proceed to Login page."
        };
    }

    public async Task<User> GetUserByClaims(ClaimsPrincipal userClaims)
    {
        return await _userManager.FindByIdAsync(userClaims.FindFirstValue(ClaimTypes.Sid));
    }

    public async Task<IdentityResult> UpdateUser(UserUpdateDto model, ClaimsPrincipal userClaims)
    {
        var user = await GetUserByClaims(userClaims);
        
        _mapper.Map(model, user);

        var result = await _userManager.UpdateAsync(user);

        return result;
    }

    public async Task<IdentityResult> UpdateUserPassword(UserUpdatePasswordDto model, ClaimsPrincipal userClaims)
    {
        var user = await GetUserByClaims(userClaims);
        
        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        return result;
    }
    
    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<Response> DeleteUser(ClaimsPrincipal userClaims)
    {
        var user = await GetUserByClaims(userClaims);

        var result = await _userManager.DeleteAsync(user);
        
        if(result.Succeeded)
            return new Response()
            {
                Message = "Your account has been deleted successfully."
            };

        throw new ApiException("An error occured.");
    }
    
    private async Task<JwtSecurityToken> GetToken(User user)
    {
        await UpdateUserRole(user);
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Sid, user.Id.ToString()),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Surname, user.FirstName + " " + user.LastName),
            new (ClaimTypes.Email, user.Email!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        
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

    private async Task UpdateUserRole(User user)
    {
        var isPaidUser = await _usersRepository.IsUserPaidUser(user);

        await _userManager.RemoveFromRoleAsync(user, UserRoles.PaidUser);
        
        if (isPaidUser)
            await _userManager.AddToRoleAsync(user, UserRoles.PaidUser);
        else
            await _userManager.AddToRoleAsync(user, UserRoles.User);
    }
}