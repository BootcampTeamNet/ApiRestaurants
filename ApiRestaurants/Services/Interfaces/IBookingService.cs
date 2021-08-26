using DTOs.Bookings;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking);
        Task<Booking> GetById(int id);
        Task<int> ConfirmById(int id);
        Task<int> CancelByRestaurant(int id);

        Task<List<BookingListResponseDto>> ListById(int id);
    }
}
