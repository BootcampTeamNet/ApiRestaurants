using System.ComponentModel.DataAnnotations;

namespace DTOs.Users
{
    public class UserDto
    {
        [Required(ErrorMessage ="El Nombre es requerido")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El Password es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El Mobile es requerido")]
        public string Mobile { get; set; }
    }
}
