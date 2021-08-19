using DTOs.Restaurant;
using DTOs.Users;
using Entities;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserRestaurantService
    {
        Task<int> Add(RegisterRestaurantRequestDto restaurantRequestDto);
        Task<int> Update(UpdateRestaurantUserRequestDto updateRestaurantUserRequestDto);
    }
}