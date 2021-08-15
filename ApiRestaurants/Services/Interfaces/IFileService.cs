using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFileService
    {
        Task SaveFile(IFormFile file, string subDirectory);
        void DeleteFile(string path);
    }
}
