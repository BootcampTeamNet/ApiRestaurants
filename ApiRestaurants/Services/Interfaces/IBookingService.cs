using Entities;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking);
        Task<Booking> GetById(int id);
        Task<int> ConfirmById(int id);
    }
}
