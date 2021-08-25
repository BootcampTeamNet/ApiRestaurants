using DTOs.Restaurant;
using Entities;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> MakeBooking(MakeBookingRequestDto makeBooking);
    }
}
