using Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsUser(string email);
        Task<User> GetUser(string email);
    }
}
