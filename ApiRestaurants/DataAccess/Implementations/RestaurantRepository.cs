using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantsDbContext _context;

        public RestaurantRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> RestaurantsByCoordinates(double customerLatitude, double customerLongitude) {
            //1 milla - 1.609344 km
            //1 grado - 60 min
            //1 milla náutica =  1.1515 millas terrestres
            int distanceKm = 5;
            double gradeToRadian = (Math.PI / 180);
            double radianToGrade = (180 / Math.PI);
            double miles = double.Parse("1.1515", CultureInfo.GetCultureInfo("es-US"));
            double milesToKilometers = double.Parse("1.609344", CultureInfo.GetCultureInfo("en-US"));

            List<Restaurant> closestRestaurant = new List<Restaurant>();
            closestRestaurant = await (from restaurant in _context.Restaurants
                                       select new
                                       {
                                           restaurant.Id,
                                           restaurant.Name,
                                           restaurant.Address,
                                           restaurant.LocationLatitude,
                                           restaurant.LocationLongitude,
                                           restaurant.Phone,
                                           restaurant.PathImage,
                                           //La Formula de Haversine
                                           Distance =
                                           (Math.Acos(
                                                 Math.Sin(customerLatitude * gradeToRadian) *
                                                 Math.Sin(Convert.ToDouble(restaurant.LocationLatitude) * gradeToRadian) +
                                                 Math.Cos(customerLatitude * gradeToRadian) *
                                                 Math.Cos(Convert.ToDouble(restaurant.LocationLatitude) * gradeToRadian) *
                                                 Math.Cos((customerLongitude - Convert.ToDouble(restaurant.LocationLongitude)) * gradeToRadian)
                                                 ) * radianToGrade) * 60 * miles * milesToKilometers
                                       }).Where(w => w.Distance < distanceKm)
                                         .Select(s => new Restaurant
                                         {
                                             Id = s.Id,
                                             Name = s.Name,
                                             Address = s.Address,
                                             LocationLatitude = s.LocationLatitude,
                                             LocationLongitude = s.LocationLongitude,
                                             Phone = s.Phone,
                                             PathImage = s.PathImage
                                         }
                                    ).ToListAsync();

            return closestRestaurant;
        }
    }
}
