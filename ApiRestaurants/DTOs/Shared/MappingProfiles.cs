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
            CreateMap<Restaurant, RestaurantIdDto>().ReverseMap();
        }

    }
}