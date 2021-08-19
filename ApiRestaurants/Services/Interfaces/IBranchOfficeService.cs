using DTOs.Restaurant;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBranchOfficeService
    {
        Task<int> Create(BranchOfficeRequestDto branchOfficeRequestDto);
    }
}
