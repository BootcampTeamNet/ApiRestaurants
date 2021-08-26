using DTOs.Bookings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDashboardService
    {
        Task<List<BestSellingDishesResponseDto>> GetBestBookingList(int restaurantId);
    }
}
