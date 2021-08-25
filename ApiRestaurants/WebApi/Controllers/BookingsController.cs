using DTOs.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> MakeBooking(MakeBookingRequestDto makeBooking)
        {
            var response = await _bookingService.MakeBooking(makeBooking);
            return Ok(response);
        }
    }
}
