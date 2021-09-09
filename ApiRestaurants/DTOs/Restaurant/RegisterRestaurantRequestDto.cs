using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Restaurant
{
    public class RegisterRestaurantRequestDto
    {
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La Dirección es requerido")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La Latitud es requerido")]
        public double LocationLatitude { get; set; }

        [Required(ErrorMessage = "La Longitud es requerido")]
        public double LocationLongitude { get; set; }

        public IFormFile Image { get; set; }
        public RegisterUserRequestDto User { get; set; }
    }
}
