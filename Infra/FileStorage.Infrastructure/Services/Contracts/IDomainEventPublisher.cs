using FileStorage.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services.Contracts
{
    public interface IDomainEventPublisher
    {
        Task Publish(IDomainEvent @event, CancellationToken cancellationToken = default);
        Task PublishAll(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default);
    }
}
