using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Bookings;
using Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Bookings
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<List<BestSellingDishesResponseDto>> GetBestBookingList(int restaurantId)
        {
            List<BestSellingDishes> bestSelling = await _bookingRepository.GetBestBookingList(restaurantId);
            List<BestSellingDishesResponseDto> bestDto = _mapper.Map<List<BestSellingDishesResponseDto>>(bestSelling);
            return bestDto;
        }
    }
}
