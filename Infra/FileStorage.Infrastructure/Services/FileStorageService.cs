using Azure.Storage.Blobs;
using FileStorage.Application.Services.Contracts;
using FileStorage.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobClient;
        private readonly IBlobContainerNameProvider _blobContainerNameProvider;

        public FileStorageService(BlobServiceClient blobClient,
            IBlobContainerNameProvider blobContainerNameProvider)
        {
            (_blobClient, _blobContainerNameProvider) = (blobClient, blobContainerNameProvider);
        }

        public async Task UploadFileAsync(string userId, 
            string fileName, 
            Stream stream, 
            CancellationToken cancellationToken = default)
        {
            var containerName = _blobContainerNameProvider.Get(userId);
            var container = _blobClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);
            await blob.UploadAsync(stream, cancellationToken);
        }
    }
}
