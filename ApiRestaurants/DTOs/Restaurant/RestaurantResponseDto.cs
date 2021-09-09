using System;

namespace DTOs.Restaurant
{
    public class RestaurantResponseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string PathImage { get; set; }
        public int? TimeMaxCancelBooking { get; set; }
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }
        public int RestaurantCategoryId { get; set; }
        public UserResponseDto User { get; set; }
    }
}
