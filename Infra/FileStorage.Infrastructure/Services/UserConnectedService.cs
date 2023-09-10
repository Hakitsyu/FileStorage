using FileStorage.Application;
using FileStorage.Application.Services.Contracts;
using FileStorage.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class UserConnectedService : IUserConnectedService
    {
        private readonly IHubContext<UserHub, IUserConnected> _hubContext;

        public UserConnectedService(IHubContext<UserHub, IUserConnected> hubContext)
        {
            _hubContext = hubContext;
        }

        public IUserConnected GetUserConnectedById(string id)
        {
            return _hubContext.Clients.User(id);
        }
    }
}
