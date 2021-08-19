using DTOs.Restaurant;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchOfficeController : ControllerBase
    {
        private readonly IBranchOfficeService _branchOfficeService;

        public BranchOfficeController(IBranchOfficeService branchOfficeService)
        {
            _branchOfficeService = branchOfficeService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(BranchOfficeRequestDto branchOfficeRequestDto)
        {
            try
            {
                var response = await _branchOfficeService.Create(branchOfficeRequestDto);
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
