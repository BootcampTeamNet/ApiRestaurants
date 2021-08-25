using DTOs.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<BestSellingDishesResponseDto>> GetBestBookingList(int restaurantId);
    }
}
