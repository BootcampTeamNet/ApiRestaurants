using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBranchOfficeService
    {
        Task<int> Create(BranchOfficeRequestDto branchOfficeRequestDto);
        Task<List<BranchResponseDto>> GetByRestaurantId(int id);
    }
}
