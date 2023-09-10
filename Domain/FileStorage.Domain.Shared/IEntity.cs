using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Domain.Shared
{
    public interface IEntity<TId> : IEntity
    {
        TId Id { get; }
    }

    public interface IEntity
    {

    }
}
