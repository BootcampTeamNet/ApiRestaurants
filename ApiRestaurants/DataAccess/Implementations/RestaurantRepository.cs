using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public async Task<List<Restaurant>> RestaurantsByCoordinates(double customerLatitude, double customerLongitude) 
        {
            List<Restaurant> closestRestaurant = await FindByCoordinates(customerLatitude, customerLongitude).ToListAsync();
            return closestRestaurant;
        }
        public async Task<List<Restaurant>> RestaurantsByKeyWord(double customerLatitude, double customerLongitude, string keyWord)
        {
            IQueryable<Restaurant> restaurants = FindByCoordinates(customerLatitude, customerLongitude);

            List<Restaurant> closestRestaurant = await (from restaurant in restaurants.AsQueryable()
                                                        //se hace left join porque pueden existir restaurantes que aún no tienen platos
                                                        join dish in _context.Dishes on restaurant.Id equals dish.RestaurantId into lfdish
                                                        from leftdish in lfdish.DefaultIfEmpty()
                                                        join dishcategory in _context.DishCategories on leftdish.DishCategoryId equals dishcategory.Id into lfdishcategory
                                                        from leftdishcategory in lfdishcategory.DefaultIfEmpty()
                                                        where (
                                                                  restaurant.Name.Contains(keyWord)
                                                                  || leftdish.Name.Contains(keyWord)
                                                                  || leftdish.Description.Contains(keyWord)
                                                                  || leftdishcategory.Name.Contains(keyWord)
                                                              )
                                                        select restaurant
                                                        ).Distinct().ToListAsync();

            return closestRestaurant;
        }
        public IQueryable<Restaurant> FindByCoordinates(double customerLatitude, double customerLongitude)
        {
            //1 milla - 1.609344 km
            //1 grado - 60 min
            //1 milla náutica =  1.1515 millas terrestres
            int distanceKm = 5;
            double gradeToRadian = (Math.PI / 180);
            double radianToGrade = (180 / Math.PI);
            double miles = double.Parse("1.1515", CultureInfo.GetCultureInfo("es-US"));
            double milesToKilometers = double.Parse("1.609344", CultureInfo.GetCultureInfo("en-US"));

            var response = (from restaurant in _context.Restaurants
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
                              });
            return response;
        }
    }
}
