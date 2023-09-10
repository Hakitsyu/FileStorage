﻿using FileStorage.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Events
{
    public record FileEvent(Guid FileId) : DomainEvent;
}
