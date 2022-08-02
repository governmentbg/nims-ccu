using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Eumis.Common.Db
{
    public interface IUnitOfWorkHidden
    {
        DbContext DbContext { get; }

        void BulkInsert<TEntity>(IEnumerable<TEntity> items, string tableNameOverride)
            where TEntity : class;
    }
}
