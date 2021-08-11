using DTOs.Restaurant;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<int> Create(RestaurantRequestDto restaurantRequestDto);
    }

}
