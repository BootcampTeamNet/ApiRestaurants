using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Services.Implementations.Shared
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;        
        public FileService(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
        }

        public async Task SaveFile(
            IFormFile file, 
            string subDirectory
            )
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine($"{_configuration.GetSection("FileServer:path").Value}", subDirectory);

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
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
