using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Users
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserLoginResponseDto User { get; set; }
        public RestaurantLoginResponseDto Restaurant { get; set; }
    }
}
