namespace DTOs.Users
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public LoginUserResponseDto User { get; set; }
        public LoginRestaurantResponseDto Restaurant { get; set; }
    }
}
