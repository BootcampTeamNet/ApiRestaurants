using DTOs.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<int> Create(RestaurantRequestDto restaurantRequestDto);
    }

}
