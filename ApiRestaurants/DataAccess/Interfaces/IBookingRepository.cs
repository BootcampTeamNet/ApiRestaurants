using Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBookingRepository
    {
        Task<int> AddDetail(List<BookingDetail> detail);
    }
}
