using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBookingStatusRepository
    {
        Task<BookingStatus> GetByName(string name);
    }
}
