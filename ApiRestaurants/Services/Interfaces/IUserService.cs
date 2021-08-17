using DTOs.Users;
using Entities;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<int> Register(UserDto userDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> ExistsUser(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task UpdatePassword(PasswordUserDto passWordDto);
    }
}