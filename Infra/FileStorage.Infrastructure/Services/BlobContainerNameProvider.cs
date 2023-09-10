using FileStorage.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class BlobContainerNameProvider : IBlobContainerNameProvider
    {
        public string Get(string userId)
        {
            /*
                I know, this is not recommended at all but it was done this way just for testing purposes. 
                This rule for getting the name of a container is completely invalid.
             */

            return userId.Contains("@")
                ? userId.Split("@")[0]
                : userId;
        }
    }
}
