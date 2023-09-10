using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services.Contracts
{
    public interface IBlobContainerNameProvider
    {
        string Get(string userId);
    }
}
