using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

        public class ListByRestaurant {
            public int Id { get; set; }
            public DateTime OrderDate { get; set; }
            public int NumberPeople { get; set; }
            public bool IsActive { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Mobile { get; set; }
        } 
        public async Task<List<ListByRestaurant>> ListById(int id) {
            //ListById Restaurant
            var innerJoinQuery = await (from booking in _context.Bookings
                                                      join user in _context.Users on booking.UserId equals user.Id
                                                      join bookingStatus in _context.BookingStatus on  booking.BookingStatusId equals bookingStatus.Id
                                                      where booking.RestaurantId == id
                                                      select new ListByRestaurant{ 
                                                        Id = booking.RestaurantId,
                                                        OrderDate = booking.OrderDate,
                                                        NumberPeople = booking.NumberPeople,
                                                        IsActive = bookingStatus.IsActive,
                                                        FirstName = user.FirstName,
                                                        LastName = user.LastName,
                                                        Mobile = user.Mobile
                                                      }).ToListAsync();
            return innerJoinQuery;
        }
    }
}
