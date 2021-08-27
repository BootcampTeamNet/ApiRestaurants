using AutoMapper;
using DTOs.Bookings;
using DTOs.Dish;
using DTOs.Restaurant;
using DTOs.Users;
using Entities;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
       public MappingProfiles() 
        {
            CreateMap<Dish, DishRequestDto>().ReverseMap();
            CreateMap<Dish, DishResponseDto>().ReverseMap();
            CreateMap<DishCategory, DishCategoryRequestDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantResponseDto>().ReverseMap();
            CreateMap<RestaurantCategory, RestaurantCategoryRequestDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantMobileResponseDto>().ReverseMap();
            CreateMap<Dish, DishesByRestaurantResponseDto>().ReverseMap();
            CreateMap<Restaurant, BranchOfficeRequestDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<BestSellingDishes, BestSellingDishesResponseDto>().ReverseMap();
            CreateMap<BookingListResponseDto, BookingCustomer>().ReverseMap();
            CreateMap<BookingStatusResponseDto, BookingStatus>().ReverseMap();
        }
    }
}