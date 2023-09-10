using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services.Contracts
{
    public interface IBlobServiceClientProvider
    {
        BlobServiceClient Get();
    }
}
