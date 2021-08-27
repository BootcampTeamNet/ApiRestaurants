using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Bookings
{
    public class FilterBookingRequestDto
    {
        [Required (ErrorMessage ="La Fecha Desde es requerida")]
        public DateTime DateFrom { get; set; }
        [Required(ErrorMessage = "La Fecha Hasta es requerida")]
        public DateTime DateTo { get; set; }
        public List<int> BookingStatusId { get; set; }
    }
}
