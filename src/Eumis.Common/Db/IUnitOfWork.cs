using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Common.Db
{
    public interface IUnitOfWork : IDisposable
    {
        DbContextTransaction BeginTransaction();

        DbContextTransaction BeginTransaction(IsolationLevel isolationLevel);

        Task<IDisposable> AcquireLockAsync(string lockName, CancellationToken cancellationToken = default(CancellationToken));

        void Save();

        Task SaveAsync(CancellationToken ct);

        void BulkInsert<TEntity>(IEnumerable<TEntity> items)
            where TEntity : class;

        void BulkUpdate<TEntity>(IEnumerable<TEntity> items, params Expression<Func<TEntity, object>>[] properties)
            where TEntity : class;

        void BulkDelete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;
    }
}
