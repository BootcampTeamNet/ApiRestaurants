using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<List<Restaurant>> GetFavouriteList(int userId);
    }
}
