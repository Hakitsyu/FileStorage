using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class EmailUserIdProvider : IUserIdProvider
    {
        private readonly Application.Services.Contracts.IUserIdProvider _userIdProvider;

        public EmailUserIdProvider(Application.Services.Contracts.IUserIdProvider userIdProvider)
        {
            _userIdProvider = userIdProvider;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            return _userIdProvider.Get(connection.User);
        }
    }
}
