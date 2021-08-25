using DataAccess.Interfaces;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.Threading.Tasks;

namespace Services.Implementations.Bookings
{
    public class BookingStatusService: IBookingStatusService
    {
        private readonly IGenericRepository<BookingStatus> _genericRepository;
        private readonly IBookingStatusRepository _bookingStatusRepository;

        public BookingStatusService(IGenericRepository<BookingStatus> genericRepository,
            IBookingStatusRepository bookingStatusRepository)
        {
            _genericRepository = genericRepository;
            _bookingStatusRepository = bookingStatusRepository;
        }

        public async Task<BookingStatus> GetById(int id) 
        {
            BookingStatus bookingStatus = await _genericRepository.GetByIdAsync(id);
            if (bookingStatus == null) {
                throw new EntityNotFoundException($"El status con el id {id} no existe");
            }
            return bookingStatus;
        }

        public async Task<BookingStatus> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name)) {
                throw new EntityBadRequestException($"El parámetro name está vacío");
            }
            BookingStatus bookingStatus = await _bookingStatusRepository.GetByName(name);
            if (bookingStatus == null)
            {
                throw new EntityNotFoundException($"El status con el nombre {name} no existe");
            }
            return bookingStatus;
        }
    }
}
