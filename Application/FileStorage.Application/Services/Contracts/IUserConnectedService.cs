﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Services.Contracts
{
    public interface IUserConnectedService
    {
        public IUserConnected GetUserConnectedById(string id);
    }
}
