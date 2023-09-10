using FileStorage.Domain.Shared;
using FileStorage.Infrastructure.Services.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Services
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IMediator _mediator;

        public DomainEventPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(IDomainEvent @event, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(@event, cancellationToken);
        }

        public async Task PublishAll(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
        {
            foreach (var @event in events)
            {
                await Publish(@event, cancellationToken);
            }
        }
    }
}
