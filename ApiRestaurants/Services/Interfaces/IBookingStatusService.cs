using DTOs.Bookings;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingStatusService
    {
        Task<BookingStatus> GetByName(string name);
        Task<List<BookingStatusResponseDto>> GetAll();
    }
}
