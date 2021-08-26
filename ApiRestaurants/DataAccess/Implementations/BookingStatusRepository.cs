using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class BookingStatusRepository : IBookingStatusRepository
    {
        private readonly RestaurantsDbContext _context;
        public BookingStatusRepository(RestaurantsDbContext context)
        {
            _context = context;
        }
        public async Task<BookingStatus> GetByName(string name)
        {
            return await _context.BookingStatus.Where(w => w.Name.ToUpper() == name.ToUpper()).FirstOrDefaultAsync();
        }
    }
}
