using AutoMapper;
using DTOs.Dish;
using Entities;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
       public MappingProfiles() 
        {
            CreateMap<Dish, DishRequestDto>().ReverseMap();
            CreateMap<DishCategory, DishCategoryRequestDto>().ReverseMap();
        }
    }
}