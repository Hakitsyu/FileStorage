using FileStorage.Domain.Enums;
using FileStorage.Domain.Events;
using FileStorage.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Entities
{
    public class File : AggregateRoot<Guid>
    {
        public string Name { get; }
        public string? UserId { get; }
        public FileStatus Status { get; private set; }

        private File(string name, string? userId)
        {
            Id = Guid.NewGuid();
            Status = FileStatus.Pending;
            Name = name;
            UserId = userId;
        }

        public void Success()
        {
            Status = FileStatus.Success;
            AddDomainEvent(new FinishedFileEvent(Id, FileStatus.Success));
        }

        public void Failure()
        {
            Status = FileStatus.Failure;
            AddDomainEvent(new FinishedFileEvent(Id, FileStatus.Failure));
        }

        public bool IsFinished()
        {
            return Status != FileStatus.Pending;
        }

        public static class Factory
        {
            public static File Create(string name, string? userId = null)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                var entity = new File(name, userId);
                entity.AddDomainEvent(new CreatedFileEvent(entity.Id));
                return entity;
            }
        }
    }
}
