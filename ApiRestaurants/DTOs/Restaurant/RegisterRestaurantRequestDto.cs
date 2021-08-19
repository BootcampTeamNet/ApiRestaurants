namespace DTOs.Restaurant
{
    public class RegisterRestaurantRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public RegisterUserRequestDto User { get; set; }
    }
}
