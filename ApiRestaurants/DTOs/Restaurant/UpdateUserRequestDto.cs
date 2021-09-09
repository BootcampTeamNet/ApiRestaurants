using System.ComponentModel.DataAnnotations;

namespace DTOs.Restaurant
{
    public class UpdateUserRequestDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
