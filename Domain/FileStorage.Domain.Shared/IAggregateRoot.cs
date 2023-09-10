using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Shared
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> GetDomainEvents();
        void AddDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }
}
