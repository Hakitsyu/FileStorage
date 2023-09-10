using FileStorage.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Services
{
    public interface IFileService
    {
        Task UploadAsync(UploadFileCommand command);
    }
}
