using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Services.Contracts
{
    public interface IUserIdProvider
    {
        string Get(ClaimsPrincipal user);
    }
}
