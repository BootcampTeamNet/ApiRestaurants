using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRestaurantRepository
    {
        Task<UserRestaurant> GetByUserId(int id);
        Task<int> Add(UserRestaurant userRestaurant);
    }
}
