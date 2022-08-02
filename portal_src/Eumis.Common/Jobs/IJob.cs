using System;

namespace Eumis.Common.Jobs
{
    public interface IJob
    {
        string Name { get; }
        TimeSpan Period { get; }
        void Action();
    }
}