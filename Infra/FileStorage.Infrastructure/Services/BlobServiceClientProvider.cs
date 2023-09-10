using Azure.Identity;
using Azure.Storage.Blobs;
using FileStorage.Infrastructure.Configurations;
using FileStorage.Infrastructure.Services.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class BlobServiceClientProvider : IBlobServiceClientProvider
    {
        private readonly ITokenCredentialProvider _credentialProvider;
        private readonly AzureConfiguration _configuration;

        private BlobServiceClient? _value;

        public BlobServiceClientProvider(IOptions<AzureConfiguration> configuration,
            ITokenCredentialProvider tokenCredentialProvider)
        {
            (_configuration, _credentialProvider) = (configuration.Value, tokenCredentialProvider);
        }

        public BlobServiceClient Get()
        {
            return _value ??= new BlobServiceClient(new Uri(_configuration.Storage.Uri),
                _credentialProvider.Get());
        }
    }
}
