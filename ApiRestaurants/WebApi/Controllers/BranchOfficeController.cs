using DTOs.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/branches")]
    [ApiController]
    [Authorize]
    public class BranchOfficeController : ControllerBase
    {
        private readonly IBranchOfficeService _branchOfficeService;

        public BranchOfficeController(IBranchOfficeService branchOfficeService)
        {
            _branchOfficeService = branchOfficeService;
        }

        /// <summary>
        /// Create a new branch for RestaurantId. Name e.g South, North 
        /// </summary>
        /// <param name="branchOfficeRequestDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Create(BranchOfficeRequestDto branchOfficeRequestDto)
        {
            try
            {
                var response = await _branchOfficeService.Create(branchOfficeRequestDto);
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
        /// Get branches by principal RestaurantId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("restaurants/id")]
        public async Task<IActionResult> GetByRestaurantId(int id)
        {
            var response = await _branchOfficeService.GetByRestaurantId(id);
            return Ok(response);
        }

    }
}
