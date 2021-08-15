using DataAccess;
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
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IUserRestaurantService _userRestaurantService;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public UserService(IGenericRepository<User> genericRepository, IUserRepository userRepository,
            IPasswordService passwordService, IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _userRepository = userRepository;
            //_userRestaurantService = userRestaurantService;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        public async Task<bool> ExistsUser(string email)
        {
            return await _userRepository.ExistsUser(email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            string email = loginRequestDto.Email.Trim();
            string password = loginRequestDto.Password.Trim();

            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Error, debe ingresar un correo electrónico");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Error, debe ingresar una contraseña");
            }

            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("Error, usuario no existe");
            }
            if (!_passwordService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Error, contraseña incorrecta");
            }

            LoginResponseDto loginResponseDto = new LoginResponseDto();
            //loginResponseDto.Restaurant = await _userRestaurantService.GetByUserId(user.Id);
            if(loginResponseDto.Restaurant==null) {
                loginResponseDto.User = new LoginUserResponseDto
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    Email = user.Email
                };
            }

            loginResponseDto.Token = CreateToken(user);
            return loginResponseDto;
        }

        public async Task<int> Register(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name))
            {
                throw new Exception("Error, debe ingresar su nombre");
            }

            if (userDto.Name.Trim().Length == 0)
            {
                throw new Exception("Error, debe ingresar su nombre");
            }

            if (string.IsNullOrEmpty(userDto.Mobile))
            {
                throw new Exception("Error, debe ingresar su número de dispositivo móvil");
            }

            if (userDto.Mobile.Trim().Length == 0)
            {
                throw new Exception("Error, debe ingresar su número de dispositivo móvil");
            }

            if (string.IsNullOrEmpty(userDto.Password))
            {
                throw new Exception("Error, debe  asignar una contraseña");
            }

            if (userDto.Password.Trim().Length == 0)
            {
                throw new Exception("Error, debe asignar una contraseña");
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

            var result = await _genericRepository.Add(user);

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

        public async Task UpdatePassword(PasswordUserDto passwordDto)
        {
            var user =await _userRepository.GetUserByEmail(passwordDto.Email);
            if (!await ExistsUser(passwordDto.Email)) 
            {
                throw new Exception("Error, no existe el usuario");
            }
            if (!string.IsNullOrEmpty(passwordDto.Password))
            {
                var verifyPassword = _passwordService.VerifyPasswordHash(passwordDto.Password, user.PasswordHash, user.PasswordSalt);
                if (!verifyPassword)
                {
                    _passwordService.CreatePasswordHash(passwordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }
            }

            await _genericRepository.Update(user);
        }
    }
}