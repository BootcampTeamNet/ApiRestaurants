using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Restaurant
{
    public class UpdateRestaurantUserRequestDto
    {
        [Required(ErrorMessage = "La Dirección es requerido")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La Latitud es requerido")]
        public string LocationLatitude { get; set; }

        [Required(ErrorMessage = "La Longitud es requerido")]
        public string LocationLongitude { get; set; }

        public int? TimeMaxCancelBooking { get; set; }
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }
        
        [Required]
        public int RestaurantCategoryId { get; set; }
        public IFormFile Image { get; set; }
        public UpdateUserRequestDto User { get; set; }
    }
}