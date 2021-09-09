using Microsoft.AspNetCore.Http;
using System;

namespace DTOs.Restaurant
{
    public class UpdateRestaurantUserRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public int? TimeMaxCancelBooking { get; set; }
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }
        public int RestaurantCategoryId { get; set; }
        public IFormFile Image { get; set; }
        public UpdateUserRequestDto User { get; set; }
    }
}