using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<BookingCustomer>> ListByRestaurantId(int id);
    }
}
