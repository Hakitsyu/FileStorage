using FileStorage.Domain.Repositories;
using FileStorage.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Infrastructure.Persistance.Repositories
{
    public class InMemoryFileRepository : IFileRepository
    {
        private readonly List<Domain.Entities.File> _files;
        private readonly IDomainEventPublisher _domainEventPublisher;

        public InMemoryFileRepository(IDomainEventPublisher domainEventPublisher)
        {
            _files = new();
            _domainEventPublisher = domainEventPublisher;
        }

        public async Task AddAsync(Domain.Entities.File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (!_files.Any(f => f.Id == file.Id))
            {
                _files.Add(file);

                var events = file.GetDomainEvents();
                file.ClearDomainEvents();
                await _domainEventPublisher.PublishAll(events);
            }
        }

        public async Task UpdateAsync(Domain.Entities.File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var foundFile = _files.FirstOrDefault(f => f.Id == file.Id);
            if (foundFile != null)
            {
                _files.Remove(foundFile);
                _files.Add(file);

                var events = file.GetDomainEvents();
                file.ClearDomainEvents();
                await _domainEventPublisher.PublishAll(events);
            }
        }

        public Task<Domain.Entities.File?> GetByIdAsync(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));

            return Task.FromResult(_files.FirstOrDefault(f => f.Id == id));
        }
    }
}
