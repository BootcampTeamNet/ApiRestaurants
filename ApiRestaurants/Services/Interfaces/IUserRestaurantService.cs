using DTOs.Restaurant;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserRestaurantService
    {
        Task<int> Add(RegisterRestaurantRequestDto restaurantRequestDto);
    }
}
