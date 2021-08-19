namespace DTOs.Users
{
    public class LoginRestaurantResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LoginUserResponseDto User { get; set; }
    }
}
