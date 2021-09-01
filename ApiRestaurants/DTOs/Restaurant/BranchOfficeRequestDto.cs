using System.ComponentModel.DataAnnotations;

namespace DTOs.Restaurant
{
    public class BranchOfficeRequestDto
    {
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La Dirección es requerido")]
        public string Address { get; set; }
        [Required(ErrorMessage = "La Latitud es requerido")]
        public double LocationLatitude { get; set; }
        [Required(ErrorMessage = "La Longitud es requerido")]
        public double LocationLongitude { get; set; }
        [Required(ErrorMessage = "El Id del Restaurante principal es requerido")]
        public int MainBranchId { get; set; }
        public RegisterUserRequestDto User { get; set; }
    }
}
