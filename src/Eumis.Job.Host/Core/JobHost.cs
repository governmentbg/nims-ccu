using Eumis.Common.Log;
using System;
using System.Threading;

namespace Eumis.Job.Host.Core
{
    public class JobHost : IDisposable
    {
        private readonly object jobLock = new object();

        private IJob job;
        private ILogger logger;
        private Timer timer;

        public JobHost(ILogger logger)
        {
            this.logger = logger;
        }

        // reads & writes of bool are atomic, so no lock is required
        public bool IsShuttingDown { get; private set; }

        public void Start(IJob job, CancellationToken token)
        {
            this.job = job ?? throw new ArgumentNullException(nameof(job));
            this.logger.Log(LogLevel.Info, string.Format("Initializing {0}", this.job.Name));
            this.timer = new Timer((sender) => this.DoAction(token));
            this.timer.Change(TimeSpan.FromSeconds(0), this.job.Period);
        }

        private void DoAction(CancellationToken token)
        {
            if (this.IsShuttingDown)
            {
                return;
            }

            // DoAction returns immediately if the previous action has not finished
            if (Monitor.TryEnter(this.jobLock))
            {
                try
                {
                    this.job.Action(token);
                }
                catch (Exception e)
                {
                    this.logger.Log(LogLevel.Error, string.Format("Error while running {0}", this.job.Name), e);
                }
                finally
                {
                    Monitor.Exit(this.jobLock);
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }

            if (this.job != null)
            {
                this.job.Dispose();
                this.logger.Log(LogLevel.Info, string.Format("Disposed {0}", this.job.Name));
                this.job = null;
            }
        }
    }
}