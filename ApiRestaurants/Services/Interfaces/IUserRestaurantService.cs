using DTOs.Restaurant;
using DTOs.Users;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserRestaurantService
    {
        Task<int> Add(RegisterRestaurantRequestDto restaurantRequestDto);
        Task<LoginRestaurantResponseDto> GetByUserId(int id);
    }
}
