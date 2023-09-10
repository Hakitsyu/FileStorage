using Azure.Core;
using Azure.Identity;
using FileStorage.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class TokenCredentialProvider : ITokenCredentialProvider
    {
        public TokenCredential Get() => new DefaultAzureCredential();
    }
}
