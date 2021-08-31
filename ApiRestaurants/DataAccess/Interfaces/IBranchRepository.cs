using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBranchRepository
    {
        Task<List<Restaurant>> GetByRestaurantId(int id);
    }
}
