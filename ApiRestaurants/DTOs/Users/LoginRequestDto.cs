using System.ComponentModel.DataAnnotations;

namespace DTOs.Users
{
    public class LoginRequestDto
    {
        [EmailAddress(ErrorMessage ="Ingrese un correo electrónico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Password es requerido")]
        public string Password { get; set; }
    }
}