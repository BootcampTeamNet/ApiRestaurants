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

    public class UserResponseDto {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
