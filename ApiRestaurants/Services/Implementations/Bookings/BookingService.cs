using DataAccess.Interfaces;
using DTOs.Constants;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.Threading.Tasks;

namespace Services.Implementations.Bookings
{
    public class BookingService: IBookingService
    {
        private readonly IGenericRepository<Booking> _genericRepository;
        private readonly IBookingStatusService _bookingStatusService;
        public BookingService(IGenericRepository<Booking> genericRepository
            , IBookingStatusService bookingStatusService)
        {
            _genericRepository = genericRepository;
            _bookingStatusService = bookingStatusService;
        }

        public async Task<Booking> GetById(int id)
        {
            Booking booking = await _genericRepository.GetByIdAsync(id);
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

            return await _genericRepository.Update(booking);
        }
    }
}
