using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Shared
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected AggregateRoot()
        {

        }

        protected AggregateRoot(TId id) : base(id)
        {

        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected AggregateRoot()
        {

        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
