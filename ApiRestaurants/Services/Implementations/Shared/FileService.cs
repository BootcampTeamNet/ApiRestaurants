using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Services.Implementations.Shared
{
    public class FileService : IFileService
    {
        public async Task SaveFile(IFormFile file, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine("C:\\Desktop\\Restaurants\\", subDirectory);

            Directory.CreateDirectory(target);

            if (file.Length <= 0) return;
            var filePath = Path.Combine(target, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public void DeleteFile(string path)
        {
            Directory.Delete(path, true);
        }
    }
}
