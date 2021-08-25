using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Implementations.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingGenericRepository;
        private readonly IGenericRepository<BookingDetail> _bookingDetailGenericRepository;
        private readonly IGenericRepository<Restaurant> _restaurantGenericRepository;
        private readonly IGenericRepository<User> _userGenericRepository;

        public BookingService(IGenericRepository<Booking> bookingGenericRepository,
                              IGenericRepository<BookingDetail> bookingDetailGenericRepository,
                              IGenericRepository<Restaurant> restaurantGenericRepository,
                              IGenericRepository<User> userGenericRepository)
        {
            _bookingGenericRepository = bookingGenericRepository;
            _bookingDetailGenericRepository = bookingDetailGenericRepository;
            _restaurantGenericRepository = restaurantGenericRepository;
            _userGenericRepository = userGenericRepository;
        }

        public async Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking)
        {
            //Hacer mapeo front - DTO
            Booking booking = new Booking
            {
                NumberPeople = makeBooking.NumberPeople,
                OrderDate = makeBooking.OrderDate,
                UserId = makeBooking.UserId,
                BookingStatusId = 1,
                RestaurantId = makeBooking.RestaurantId
            };

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Do Operation 1
                int resp = await _bookingGenericRepository.Add(booking);
                if (resp > 0 && (makeBooking.DishesList != null && makeBooking.DishesList.Count > 0))
                {
                    List<BookingDetail> dishes = (from dish in makeBooking.DishesList
                                                  select new BookingDetail
                                                  {
                                                      DishId = dish.DishId,
                                                      Quantity = dish.Quantity,
                                                      Notes = dish.Notes,
                                                      BookingId = booking.Id
                                                  }).ToList();
                    // Do Operation 2
                    await _bookingDetailGenericRepository.AddRange(dishes);
                }

                // if all the coperations complete successfully, this would be called and commit the trabsaction. 
                // In case of an exception, it wont be called and transaction is rolled back
                scope.Complete();
            }

            //Hacer una busqueda y mapear a DTO
            Restaurant restaurant = await _restaurantGenericRepository.GetByIdAsync(booking.RestaurantId);
            User customer = await _userGenericRepository.GetByIdAsync(booking.UserId);
            MakeBookingResponseDto response = new MakeBookingResponseDto(
                customer.FirstName,
                booking.OrderDate,
                booking.NumberPeople,
                restaurant.TimeMaxCancelBooking
                );

            return response;
        }
    }
}
