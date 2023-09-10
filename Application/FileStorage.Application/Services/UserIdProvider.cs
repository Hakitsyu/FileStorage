using FileStorage.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Services
{
    public class UserIdProvider : IUserIdProvider
    {
        public string Get(ClaimsPrincipal user)
        {
            return user.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }
    }
}
