﻿using DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<bool>> ExistUser(string email)
        {
            var response = await _userService.ExistsUser(email);

            return response;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var response = await _userService.Register(user);

            if (response == -1)
            {
                return BadRequest(new CodeErrorResponse(404, $"Ya existe un usuario registrado con el email {user.Email}"));
            }
            var result = response;

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto user)
        {
            var response = await _userService.Login(user.Email, user.Password);

            if (response == "NoUser")
            {
                return NotFound(new CodeErrorResponse(400, $"Error, usuario no existe..."));
            }
            if (response == "WrongPassword")
            {
                return BadRequest(new CodeErrorResponse(404, $"Error, ingresó clave de acceso incorrecta..."));
            }

            var obj = new { Token=response, Message="Autenticación exitosa"};

            
            return Ok(obj);

        }
    }

}