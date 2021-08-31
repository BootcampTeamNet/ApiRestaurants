using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IToStockAFile
    {
        Task<string> SaveFile(
            byte[] content,
            string extention,
            string container, // Is a folder to save the files
            string contentType
            );
        Task<string> EditFile(
            byte[] content,
            string extention,            
            string container,
            string route,
            string contentType
            );
        Task DeleteFile(
            string route,
            string container 
            );
    }
}
