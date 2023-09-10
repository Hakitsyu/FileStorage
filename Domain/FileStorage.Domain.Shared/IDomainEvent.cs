using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Shared
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime At { get; }
    }
}
