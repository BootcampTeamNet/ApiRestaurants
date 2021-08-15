using DTOs.Users;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<int> Register(UserDto userDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> ExistsUser(string email);
        Task UpdatePassword(PasswordUserDto passWordDto);
    }
}