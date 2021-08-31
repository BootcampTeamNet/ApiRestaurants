using Microsoft.AspNetCore.Http;
//using Services.Validations;

namespace DTOs.Restaurant
{
    public class RegisterRestaurantRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; } 
        
        //[SizeImageValidation(4)]
        //public IFormFile Image { get; set; }
        public RegisterUserRequestDto User { get; set; }
    }
}
