using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/booking-status")]
    [ApiController]
    public class BookingStatusController : ControllerBase
    {
        public readonly IBookingStatusService _bookingStatusService;
        public BookingStatusController(IBookingStatusService bookingStatusService)
        {
            _bookingStatusService = bookingStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var response = await _bookingStatusService.GetAll();
            return Ok(response);
        }
    }
}
