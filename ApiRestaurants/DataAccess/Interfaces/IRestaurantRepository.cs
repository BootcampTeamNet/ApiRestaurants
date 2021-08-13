using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<int> Add(UserRestaurant userRestaurant);
        Task<Restaurant> GetById(int id);
    }

}
