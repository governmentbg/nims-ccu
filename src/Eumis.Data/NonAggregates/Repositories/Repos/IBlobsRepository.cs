using System;
using System.Collections.Generic;
using Eumis.Data.Core;

namespace Eumis.Data.NonAggregates.Repositories.Repos
{
    public interface IBlobsRepository : IRepository
    {
        void ResurrectBlobs(IEnumerable<Guid> blobKeys);
    }
}
