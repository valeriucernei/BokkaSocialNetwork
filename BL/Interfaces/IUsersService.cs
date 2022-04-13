using Common.Dtos.User;

namespace BL.Interfaces;

public interface IUsersService
{
    public Task<UserLoginResponseDto> Login(UserLoginDto model);
    public Task<UserRegisterResponseDto> Register(UserRegisterDto model);
    public Task<UserRegisterResponseDto> RegisterAdmin(UserRegisterDto model);
}