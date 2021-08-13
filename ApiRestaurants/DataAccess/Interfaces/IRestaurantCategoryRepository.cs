using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    
    public interface IRestaurantCategoryRepository 
    {
        Task<IReadOnlyList<string>> GetList();
    }
}
