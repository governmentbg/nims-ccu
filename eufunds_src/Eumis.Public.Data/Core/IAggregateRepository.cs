using System;
using Eumis.Public.Domain.Core;

namespace Eumis.Public.Data.Core
{
    public interface IAggregateRepository<TEntity> : IRepository
        where TEntity : class, IAggregateRoot
    {
        TEntity Find(Guid gid);

        TEntity FindFirstOrDefault(Guid gid);

        TEntity FindForUpdate(Guid gid, byte[] version);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        byte[] GetVersion(Guid gid);
    }
}
