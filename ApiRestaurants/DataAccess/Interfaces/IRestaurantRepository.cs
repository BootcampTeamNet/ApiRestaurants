﻿using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> RestaurantsByCoordinates(double customerLatitude, double customerLongitude);
    }

}
