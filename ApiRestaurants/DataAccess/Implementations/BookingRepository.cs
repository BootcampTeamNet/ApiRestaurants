﻿using DataAccess.Interfaces;
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
        
        public async Task<List<BestSellingDishes>> GetBestBookingList(int restaurantId)
        {
            var bestBooking = await (from detail in _context.BookingDetails
                               join dish in _context.Dishes on detail.DishId equals dish.Id
                               join booking in _context.Bookings on detail.BookingId equals booking.Id
                               where booking.RestaurantId == restaurantId
                               group detail by new { detail.DishId, dish.Name, dish.Price, booking.RestaurantId } into g
                               
                               select new BestSellingDishes
                               {
                                   DishId = g.Key.DishId,
                                   Name = g.Key.Name,
                                   Price = g.Key.Price,
                                   QuantitySum = g.Sum(detail => detail.Quantity),
                                   RestaurantId = g.Key.RestaurantId
                               }).Take(3).ToListAsync();
            bestBooking.OrderByDescending(o => o.QuantitySum).ThenByDescending(p => p.Price);
            return bestBooking;
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
