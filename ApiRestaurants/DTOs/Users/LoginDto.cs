using System.ComponentModel.DataAnnotations;

namespace DTOs.Users
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}