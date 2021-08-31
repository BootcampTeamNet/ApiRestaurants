using DTOs.Favourites;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<int> Create(FavouriteRequestDto favouriteRequestDto);
    }
}
