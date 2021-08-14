using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IBranchOfficeService
    {
        Task<int> Create(BranchOfficeRequestDto branchOfficeRequestDto);
    }
}
