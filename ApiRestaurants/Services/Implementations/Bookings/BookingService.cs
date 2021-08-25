using DataAccess.Interfaces;
using DTOs.Bookings;
using DTOs.Constants;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
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
        private readonly IBookingStatusService _bookingStatusService;

        public BookingService(IGenericRepository<Booking> bookingGenericRepository,
                              IGenericRepository<BookingDetail> bookingDetailGenericRepository,
                              IGenericRepository<Restaurant> restaurantGenericRepository,
                              IGenericRepository<User> userGenericRepository,
                              IBookingStatusService bookingStatusService)
        {
            _bookingGenericRepository = bookingGenericRepository;
            _bookingDetailGenericRepository = bookingDetailGenericRepository;
            _restaurantGenericRepository = restaurantGenericRepository;
            _userGenericRepository = userGenericRepository;
            _bookingStatusService = bookingStatusService;
        }

        public async Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking)
        {
            BookingStatus bookingStatus = await _bookingStatusService.GetByName(Constant.BookingStatus.PENDIENTE);
            //Hacer mapeo front - DTO
            Booking booking = new Booking
            {
                NumberPeople = makeBooking.NumberPeople,
                OrderDate = makeBooking.OrderDate,
                UserId = makeBooking.UserId,
                BookingStatusId = bookingStatus.Id,
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

        public async Task<Booking> GetById(int id)
        {
            Booking booking = await _bookingGenericRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new EntityNotFoundException($"La reservación con el id {id} no existe");
            }

            return booking;
        }
        public async Task<int> ConfirmById(int id)
        {
            Booking booking = await GetById(id);
            BookingStatus bookingStatus = await _bookingStatusService.GetByName(Constant.BookingStatus.CONFIRMADA);
            booking.BookingStatusId = bookingStatus.Id;

            return await _bookingGenericRepository.Update(booking);
        }
    }
}
