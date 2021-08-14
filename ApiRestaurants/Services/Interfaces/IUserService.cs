using DTOs.Users;
using Entities;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<int> Register(UserDto userDto);
        Task<LoginResponseDto> Login(string email, string password);
        Task<bool> ExistsUser(string email);
    }
}