using Autofac.Features.OwnedInstances;
using Eumis.Common;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.Projects.Repositories;
using Eumis.Job.Host.Core;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Eumis.Job.Host.Jobs.Projects
{
    public sealed class ProjectCommunicationStatusJob : IJob
    {
        private readonly Func<Owned<DisposableTuple<IUnitOfWork, IProjectCommunicationsRepository>>> dependencyFactory;
        private readonly ILogger logger;

        public ProjectCommunicationStatusJob(Func<Owned<DisposableTuple<IUnitOfWork, IProjectCommunicationsRepository>>> dependencyFactory, ILogger logger)
        {
            this.dependencyFactory = dependencyFactory;
            this.logger = logger;
            this.Period = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:ProjectCommunicationStatusJobPeriodInSeconds")));
        }

        public string Name => "ProjectCommunicationStatusJob";

        public TimeSpan Period
        {
            get;
            private set;
        }

        public void Action(CancellationToken token)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                using (var factory = this.dependencyFactory())
                {
                    var unitOfWork = factory.Value.Item1;
                    var projectCommunicationsRepository = factory.Value.Item2;

                    var communications = projectCommunicationsRepository.GetProjectCommunicationsToExpire();

                    foreach (var communication in communications)
                    {
                        token.ThrowIfCancellationRequested();

                        communication.MakeExpired();
                    }

                    unitOfWork.Save();
                    var allIds = string.Join(", ", communications.Select(s => s.ProjectCommunicationId));
                    this.logger.Log(LogLevel.Info, $"Changed status of ProjectCommunications {allIds} to Expired");
                }
            }
            catch (OperationCanceledException ex)
            {
                this.logger.Log(LogLevel.Error, $"{this.Name} was canceled due to a token cancellation request", ex);
            }
            catch (Exception ex)
            {
                this.logger.Log(LogLevel.Error, $"{this.Name} finished with error in {stopwatch.ElapsedMilliseconds} ms", ex);
                throw;
            }

            this.logger.Log(LogLevel.Debug, $"{this.Name} finished in {stopwatch.ElapsedMilliseconds} ms");
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
