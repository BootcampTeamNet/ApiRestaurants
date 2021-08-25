using Microsoft.AspNetCore.Authorization;
using DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> MakeBooking(MakeBookingRequestDto makeBooking)
        {
            try
            {
                var response = await _bookingService.MakeBooking(makeBooking);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int id) 
        {
            try
            {
                int response = await _bookingService.ConfirmById(id);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (EntityBadRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult> ListById(int id)
        {
            var response = await _bookingService.ListById(id);
            return Ok(response);
        }
    }
}
