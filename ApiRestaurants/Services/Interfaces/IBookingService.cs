using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking);
        Task<Booking> GetById(int id);
        Task<int> ConfirmById(int id);
        Task<List<BookingListResponseDto>> ListById(int id);
    }
}
