using AutoMapper;
using DTOs.Dish;
using DTOs.Restaurant;
using Entities;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
       public MappingProfiles() 
        {
            CreateMap<Dish, DishRequestDto>().ReverseMap();
            CreateMap<DishCategory, DishCategoryRequestDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantResponseDto>().ReverseMap();
            CreateMap<RestaurantCategory, RestaurantCategoryRequestDto>().ReverseMap();

        }
    }
}