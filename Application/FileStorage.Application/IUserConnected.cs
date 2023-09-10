using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application
{
    public interface IUserConnected
    {
        Task StartedUploadAsync(Guid fileId);
        Task SuccessUploadAsync(Guid fileId);
        Task FailureUploadAsync(Guid fileId);
    }
}
