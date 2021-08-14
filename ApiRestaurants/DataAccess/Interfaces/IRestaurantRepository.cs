using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetById(int id);
    }

}
