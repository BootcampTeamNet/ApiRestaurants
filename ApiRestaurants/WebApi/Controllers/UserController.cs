using DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            try
            {
                var response = await _userService.Register(user);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto user)
        {
            try
            {
                var response = await _userService.Login(user);
                return Ok(response);
            }
            catch (EntityNotFoundException  ex)
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
        [HttpPut("modify-password")]
        public async Task<ActionResult> UpdatePassword(PasswordUserDto passwordUserDto)
        {
            try
            {
                await _userService.UpdatePassword(passwordUserDto);
                return Ok();
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

        [HttpGet("{email}")]
        public async Task<ActionResult<bool>> ExistUser(string email)
        {
            var response = await _userService.ExistsUser(email);

            return response;
        }

    }

}