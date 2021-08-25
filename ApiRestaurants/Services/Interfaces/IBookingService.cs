using DTOs.Booking;
using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking);
        Task<List<BookingListResponseDto>> ListById(int id);
    }
}
