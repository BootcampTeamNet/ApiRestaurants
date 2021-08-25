using DataAccess.Interfaces;
using Entities;
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

        /*
        public async Task<int> AddDetail(List<BookingDetail> detail) {
            await _context.AddRangeAsync(detail);
            return await _context.SaveChangesAsync();
        }*/
    }
}
