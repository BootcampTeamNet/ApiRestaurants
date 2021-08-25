using DataAccess.Interfaces;

namespace DataAccess.Implementations
{
    public class BookingRepository: IBookingRepository
    {
        private readonly RestaurantsDbContext _context;      
        public BookingRepository(RestaurantsDbContext context)
        {
            _context = context;
        }
    }
}
