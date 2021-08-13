using System;

namespace Entities
{
    public class Restaurant : BaseClass
    {
        public Restaurant()
        {
            ScheduleFrom = default(DateTime);
            ScheduleTo = default(DateTime);
            CreationDate = DateTime.Now;
            IsActive = true;
            MainBranchId = null;
            RestaurantCategoryId = 1;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public int? TimeMaxCancelBooking { get; set; }
        public string PathImage { get; set; }
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? MainBranchId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public int RestaurantCategoryId { get; set; }
        public RestaurantCategory RestaurantCategory { get; set; }
    }
}
