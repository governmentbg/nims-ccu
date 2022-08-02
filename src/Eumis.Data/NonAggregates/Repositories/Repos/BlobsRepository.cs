using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Repos
{
    internal class BlobsRepository : Repository, IBlobsRepository
    {
        public BlobsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void ResurrectBlobs(IEnumerable<Guid> blobKeys)
        {
            if (blobKeys.Count() == 0)
            {
                return;
            }

            var blobKeysString = string.Join(",", blobKeys.Select(k => $"N'{k.ToString()}'"));

            this.ExecuteSqlCommand($@"
                UPDATE b SET
                    b.CreateDate = GETDATE(),
                    b.IsDeleted = 0,
                    b.DeleteDate = NULL
                FROM Blobs b
                WHERE
                    b.IsDeleted = 1 AND
                    b.[Key] IN ({blobKeysString})");

            this.ExecuteSqlCommand($@"
                UPDATE bcl SET
                    bcl.CreateDate = GETDATE(),
                    bcl.IsDeleted = 0,
                    bcl.DeleteDate = NULL
                FROM BlobContentLocations bcl
                WHERE
                    bcl.IsDeleted = 1 AND
                    bcl.BlobContentLocationId IN (
                        SELECT DISTINCT
                            b.BlobContentLocationId
                        FROM Blobs b
                        WHERE
                            b.[Key] IN ({blobKeysString}))");
        }
    }
}
