using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<Favourite> FindFavorite(Favourite favourite);
    }
}
