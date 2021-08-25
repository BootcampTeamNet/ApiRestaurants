using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class BookingRepository: IBookingRepository
    {
        private readonly RestaurantsDbContext _context;      
        public BookingRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingCustomer>> ListByRestaurantId(int id) {
            var listBooking = await (from booking in _context.Bookings
                                     join user in _context.Users on booking.UserId equals user.Id
                                     join bookingStatus in _context.BookingStatus on booking.BookingStatusId equals bookingStatus.Id
                                     where booking.RestaurantId == id
                                     select new BookingCustomer
                                     {
                                         Id = booking.Id,
                                         OrderDate = booking.OrderDate,
                                         NumberPeople = booking.NumberPeople,
                                         Status = bookingStatus.Name,
                                         FirstName = user.FirstName,
                                         Mobile = user.Mobile
                                     }).ToListAsync();
            return listBooking;
        }
    }
}
