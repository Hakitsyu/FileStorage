using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Services.Contracts
{
    public interface IFileStorageService
    {
        Task UploadFileAsync(string userId, string fileName, Stream stream, CancellationToken cancellationToken = default);
    }
}
