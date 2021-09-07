using DTOs.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IUserRestaurantService _userRestaurantService;

        public RestaurantController(IRestaurantService restaurantService, IUserRestaurantService userRestaurantService)
        {
            _restaurantService = restaurantService;
            _userRestaurantService = userRestaurantService;
        }

        /// <summary>
        /// Register restaurant and manager user
        /// </summary>
        /// <param name="restaurantRequestDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Create(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            try
            {
                var response = await _restaurantService.Create(restaurantRequestDto);
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

        /// <summary>
        /// Update data about restaurant and manager user
        /// </summary>
        /// <param name="updateRestaurantUserRequestDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRestaurantUserRequestDto updateRestaurantUserRequestDto)
        {
            try
            {
                var response = await _userRestaurantService.Update(updateRestaurantUserRequestDto);

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

        /// <summary>
        /// Get data about restaurant by restaurantId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _restaurantService.GetById(id);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// MOBILE - List restaurant by customer coordinates
        /// </summary>
        /// <param name="customerLatitude"></param>
        /// <param name="customerLongitude"></param>
        /// <returns></returns>
        [HttpGet("by-coordinates")]
        public async Task<IActionResult> GetAllByCoordinates(double customerLatitude, double customerLongitude) 
        {
            var response = await _restaurantService.GetAllByCoordinates(customerLatitude, customerLongitude);
            return Ok(response);
        }

        /// <summary>
        /// MOBILE - List restaurant by coordinates and word at least 3 character
        /// </summary>
        /// <param name="filterRequestDto"></param>
        /// <returns></returns>
        [HttpPost("by-keyword")]
        public async Task<IActionResult> GetAllByKeyWord(FilterRestaurantRequestDto filterRequestDto)
        {
            try
            {
                var response = await _restaurantService.GetAllByKeyWord(filterRequestDto);
                return Ok(response);
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
    }
}
