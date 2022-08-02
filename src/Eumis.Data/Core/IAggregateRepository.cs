using Eumis.Data.Core;
using Eumis.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data
{
    public interface IAggregateRepository<TEntity> : IRepository
        where TEntity : class, IAggregateRoot
    {
        TEntity Find(int id);

        Task<TEntity> FindAsync(int id, CancellationToken ct);

        TEntity FindForUpdate(int id, byte[] version);

        TEntity FindForUpdate(int id, string version);

        Task<TEntity> FindForUpdateAsync(int id, byte[] version, CancellationToken ct);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        TEntity FindWithoutIncludes(int id);

        Task<TEntity> FindWithoutIncludesAsync(int id, CancellationToken ct);

        byte[] GetVersion(int id);
    }
}
