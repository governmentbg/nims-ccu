using System;
using System.Threading;

namespace Eumis.Job.Host.Core
{
    public interface IJob : IDisposable
    {
        string Name { get; }

        TimeSpan Period { get; }

        void Action(CancellationToken token);
    }
}
