using Services.Interfaces;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Entities;

namespace Services.Implementations.Bookings
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGenericRepository<Booking> _bookingGenericRepository;

        public BookingService(IBookingRepository bookingRepository,
                              IGenericRepository<Booking> bookingGenericRepository,
                              IGenericRepository<BookingDetail> bookingDetailGenericRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingGenericRepository = bookingGenericRepository;
        }

        public async Task<Booking> MakeBooking(MakeBookingRequestDto makeBooking)
        {
            //Hacer mapeo front - DTO
            Booking booking = new Booking {
                NumberPeople = makeBooking.NumberPeople,
                OrderDate = makeBooking.OrderDate,
                UserId = makeBooking.UserId,
                BookingStatusId = 1,
                RestaurantId = makeBooking.RestaurantId
            };

            int idBooking = await _bookingGenericRepository.Add(booking);

            if (idBooking != 0)
            {
                List<BookingDetail> dishes = (from dish in makeBooking.DishesList
                                              select new BookingDetail
                                              {
                                                  DishId = dish.DishId,
                                                  Quantity = dish.Quantity,
                                                  Notes = dish.Notes,
                                                  BookingId = idBooking
                                              }).ToList();

                //await _bookingDetailGenericRepository.AddRange(dishes);
                await _bookingRepository.AddDetail(dishes);
            }

            //booking.Details = dishes;
            //Booking response = await _bookingRepository.MakeBooking(booking, dishes);
            //Hacer mapeo DB(IQueryable)-front(MBResponseDTO)
            return booking;
        }
    }
}
