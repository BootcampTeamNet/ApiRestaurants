using AutoMapper;
using DTOs.Bookings;
using DTOs.Dish;
using DTOs.Favourites;
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
            CreateMap<Dish, DishDto>().ReverseMap();
            CreateMap<DishCategory, DishCategoryResponseDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantResponseDto>().ReverseMap();
            CreateMap<RestaurantCategory, RestaurantCategoryResponseDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantMobileResponseDto>().ReverseMap();
            CreateMap<Dish, DishesByRestaurantResponseDto>().ReverseMap();
            CreateMap<Restaurant, BranchOfficeRequestDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<BestSellingDishes, BestSellingDishesResponseDto>().ReverseMap();
            CreateMap<BookingListResponseDto, BookingOwner>().ReverseMap();
            CreateMap<BookingStatusResponseDto, BookingStatus>().ReverseMap();
            CreateMap<FavouriteRequestDto, Favourite>().ReverseMap();
            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<BranchResponseDto, Restaurant>().ReverseMap();
        }
    }
}