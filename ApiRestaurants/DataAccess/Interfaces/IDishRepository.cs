using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDishRepository
    {
        Task<bool> ExistDish(int id);
        Task<Dish> GetDish(string id);
        Task<Dish> GetById(int id);
        Task<List<Dish>> GetListByIdRestaurant(int id);
    }
}
