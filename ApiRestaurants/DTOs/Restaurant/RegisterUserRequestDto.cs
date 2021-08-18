using System.ComponentModel.DataAnnotations;

namespace DTOs.Restaurant
{
    public class RegisterUserRequestDto
    {
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El Password es requerido")]
        public string Password { get; set; }
    }
}
