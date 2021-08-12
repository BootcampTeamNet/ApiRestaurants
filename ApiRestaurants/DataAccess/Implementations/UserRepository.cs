using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantsDbContext _restaurantsDbContext;
        public UserRepository(RestaurantsDbContext restaurantsDbContext)
        {
            _restaurantsDbContext = restaurantsDbContext;
        }

        public async Task<bool> ExistsUser(string email)
        {
            var response = false;
            response = await _restaurantsDbContext.Set<User>().AnyAsync(x => x.Email == email);
            return response;
        }

        public async Task<User> GetUser(string email)
        {
            User user = new User();
            user = await _restaurantsDbContext.Users.FirstOrDefaultAsync(
                x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }
    }
}
