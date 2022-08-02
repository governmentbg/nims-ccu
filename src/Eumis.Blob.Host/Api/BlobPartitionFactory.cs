using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Eumis.Blob.Host.Api
{
    public static class BlobPartitionFactory
    {
        private const int PartitionBase = 116;
        private const int PartitionCount = 32;

        private static readonly ThreadLocal<Random> Random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        private static int seed = Environment.TickCount;

        public static int NextValue()
        {
            return PartitionBase + (Random.Value.Next() % PartitionCount);
        }
    }
}