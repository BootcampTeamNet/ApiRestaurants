namespace DTOs.Restaurant
{
    public class FilterRestaurantRequestDto
    {
        public double CustomerLatitude { get; set; }
        public double CustomerLongitude { get; set; }
        public string KeyWord { get; set; }
    }
}
