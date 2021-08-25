using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Booking;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations.Bookings
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGenericRepository<Booking> _bookingGenericRepository;
        private readonly IGenericRepository<BookingDetail> _bookingDetailGenericRepository;
        private readonly IGenericRepository<Restaurant> _restaurantGenericRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository,
                              IGenericRepository<Booking> bookingGenericRepository,
                              IGenericRepository<BookingDetail> bookingDetailGenericRepository,
                              IGenericRepository<Restaurant> restaurantGenericRepository,
                              IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _bookingGenericRepository = bookingGenericRepository;
            _bookingDetailGenericRepository = bookingDetailGenericRepository;
            _restaurantGenericRepository = restaurantGenericRepository;
            _mapper = mapper;
        }

        public async Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto makeBooking)
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

            if (idBooking == 0)
            {
                //Falta manejar esta excepción
                throw new Exception("Excepción por error en inserción");
            }

            List<BookingDetail> dishes = (from dish in makeBooking.DishesList
                                          select new BookingDetail
                                          {
                                              DishId = dish.DishId,
                                              Quantity = dish.Quantity,
                                              Notes = dish.Notes,
                                              BookingId = booking.Id
                                          }).ToList();

            int idDishes = await _bookingDetailGenericRepository.AddRange(dishes);

            if (idDishes == 0)
            {
                //Falta manejar esta excepción
                throw new Exception("Excepción por error en inserción");
            }

            //Hacer una busqueda y mapear a DTO
            
            Booking bookingResponse = await _bookingGenericRepository.GetByIdAsync(booking.Id);
            Restaurant restaurant = await _restaurantGenericRepository.GetByIdAsync(booking.RestaurantId);

            MakeBookingResponseDto response = new MakeBookingResponseDto(
                bookingResponse.Id,
                bookingResponse.OrderDate,
                bookingResponse.NumberPeople,
                restaurant.TimeMaxCancelBooking
                );

            return response;
        }

        public async Task<List<BookingListResponseDto>> ListById(int id) {
            var list = await _bookingRepository.ListByRestaurantId(id);
            List<BookingListResponseDto> response = _mapper.Map<List<BookingListResponseDto>>(list);
            return response;
        }
    }
}
