using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingStatusService
    {
        Task<List<BookingStatus>> GetAll();
        Task<BookingStatus> GetById(int id);
        Task<BookingStatus> GetByName(string name);
    }
}
