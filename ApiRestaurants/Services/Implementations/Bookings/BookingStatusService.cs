using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Bookings;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Bookings
{
    public class BookingStatusService: IBookingStatusService
    {
        private readonly IBookingStatusRepository _bookingStatusRepository;
        private readonly IGenericRepository<BookingStatus> _bookingStatusGenericRepository;
        private readonly IMapper _mapper;

        public BookingStatusService(IBookingStatusRepository bookingStatusRepository,
                                    IGenericRepository<BookingStatus> bookingStatusGenericRepository,
                                    IMapper mapper)
        {
            _bookingStatusRepository = bookingStatusRepository;
            _bookingStatusGenericRepository = bookingStatusGenericRepository;
            _mapper = mapper;
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

        public async Task<List<BookingStatusResponseDto>> GetAll()
        {
            IReadOnlyList<BookingStatus> lista = await _bookingStatusGenericRepository.GetAllAsync();
            List<BookingStatusResponseDto> response = _mapper.Map<List<BookingStatusResponseDto>>(lista);

            return response;
        }
    }
}
