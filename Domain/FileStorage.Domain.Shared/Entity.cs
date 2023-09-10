using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Shared
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public virtual TId Id { get; protected set; } = default;

        protected Entity()
        {

        }

        protected Entity(TId id)
        {
            Id = id;
        }
    }

    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();
    }
}
