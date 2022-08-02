using System;
using System.Threading;

namespace Eumis.Public.Common.Jobs
{
    public interface IJob : IDisposable
    {
        string Name { get; }

        TimeSpan Period { get; }

        void Action(CancellationToken token);
    }
}