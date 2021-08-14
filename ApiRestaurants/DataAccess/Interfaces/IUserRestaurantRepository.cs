using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRestaurantRepository
    {
        Task<UserRestaurant> GetByUserId(int id); 
    }
}
