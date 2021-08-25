using Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataAccess.Implementations.BookingRepository;

namespace DataAccess.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<ListByRestaurant>> ListById(int id);
    }
}
