using DataAccess.Interfaces;
using DTOs.Users;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Inplementations.Users
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUserRepository _iUserRepository;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;
        public UserService(IGenericRepository<User> userRepository,
            IUserRepository iUserRepository, IPasswordService passwordService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _iUserRepository = iUserRepository;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        public async Task<bool> ExistsUser(string email)
        {
            return await _iUserRepository.ExistsUser(email);
        }

        public async Task<string> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Por favor ingresar el correo electrónico con que se registró");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Por favor ingresar el password que registró");
            }

            var user = await _iUserRepository.GetUser(email);

            if (user == null)
            {
                return "NoUser";
            }
            else if (!_passwordService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return "WrongPassword";
            }
            else
            {
                return CreateToken(user);
            }
        }

        public async Task<int> Register(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name))
            {
                throw new Exception("Por favor ingrese su nombre");
            }

            if (userDto.Name.Trim().Length == 0)
            {
                throw new Exception("Por favor ingrese su nombre");
            }

            if (string.IsNullOrEmpty(userDto.Mobile))
            {
                throw new Exception("Por favor ingrese su número de dispositivo móvil");
            }

            if (userDto.Mobile.Trim().Length == 0)
            {
                throw new Exception("Por favor ingrese su número de dispositivo móvil");
            }

            if (string.IsNullOrEmpty(userDto.Password))
            {
                throw new Exception("Por favor asigne una contraseña");
            }

            if (userDto.Password.Trim().Length == 0)
            {
                throw new Exception("Por favor asigne una contraseña");
            }

            User user = new User
            {
                FirstName = userDto.Name,
                Mobile = userDto.Mobile,
                Email = userDto.Email,
            };

            if (await ExistsUser(userDto.Email.ToLower()))
            {
                return -1;
            }

            _passwordService.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var result = await _userRepository.Add(user);

            return user.Id;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return  tokenHandler.WriteToken(token);

        }
    }
}