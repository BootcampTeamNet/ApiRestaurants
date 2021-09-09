using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRestaurantRepository
    {
        Task<int> Add(UserRestaurant userRestaurant);
        Task<int> Update(UserRestaurant userRestaurant);
        Task<UserRestaurant> GetByUserId(int id);
        Task<UserRestaurant> GetByRestaurantId(int id);
    }
}