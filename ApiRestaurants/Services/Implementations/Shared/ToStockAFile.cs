using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Shared
{
    public class ToStockAFile : IToStockAFile
    {
        private readonly string connectionString;
        public ToStockAFile(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }
        public async Task DeleteFile(
            string route, 
            string container
            )
        {
            if (string.IsNullOrEmpty(route))
            {
                return;
            }

            var client = new BlobContainerClient(
                connectionString, 
                container
                );
            await client.CreateIfNotExistsAsync();
            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(
            byte[] content, 
            string extention, 
            string container,
            string route,
            string contentType
            )
        {
            await DeleteFile(route, container);
            return await SaveFile(
                content, 
                extention, 
                container, 
                contentType
                );
        }

        public async Task<string> SaveFile(
            byte[] content,
            string extention,
            string container,
            string contentType
            )
        {
            var client = new BlobContainerClient(
                connectionString, 
                container
                );
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);

            var fileName = $"{Guid.NewGuid()}{extention}";
            var blob = client.GetBlobClient(fileName);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = contentType;

            await blob.UploadAsync(
                new BinaryData(content), 
                blobUploadOptions
                );
            return blob.Uri.ToString();
        }
    }
}
