using FileStorage.Application.Services.Contracts;
using FileStorage.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class CurrentUserIdProvider : ICurrentUserIdProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserIdProvider _userIdProvider;

        public CurrentUserIdProvider(IHttpContextAccessor httpContextAccessor,
            IUserIdProvider userIdProvider)
        {
            (_httpContextAccessor, _userIdProvider) = (httpContextAccessor, userIdProvider);
        }

        public string Get()
        {
            return _userIdProvider.Get(_httpContextAccessor.HttpContext.User);
        }
    }
}
