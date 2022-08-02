using Autofac.Features.OwnedInstances;
using Eumis.Common;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Job.Host.Core;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Eumis.Job.Host.Jobs.Procedures
{
    public class ProcedureChangeStatusJob : IJob
    {
        private readonly Func<Owned<DisposableTuple<IUnitOfWork, IProceduresRepository>>> dependencyFactory;
        private readonly ILogger logger;

        private bool disposed = false;

        public ProcedureChangeStatusJob(Func<Owned<DisposableTuple<IUnitOfWork, IProceduresRepository>>> dependencyFactory, ILogger logger)
        {
            this.dependencyFactory = dependencyFactory;
            this.logger = logger;
            this.Period = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:ProcedureTimeLimitJobPeriodInSeconds")));
        }

        public string Name => "ProcedureTimeLimitJob";

        public TimeSpan Period
        {
            get;
            private set;
        }

        public void Action(CancellationToken cancellationToken)
        {
            try
            {
                using (var factory = this.dependencyFactory())
                {
                    var unitOfWork = factory.Value.Item1;
                    var proceduresRepository = factory.Value.Item2;

                    var procedureIds = proceduresRepository.GetProceduresPassedTimeLimit();

                    if (!procedureIds.Any())
                    {
                        return;
                    }

                    foreach (var procedureId in procedureIds)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var procedure = proceduresRepository.Find(procedureId);
                        procedure.ChangeStatus(ProcedureStatus.Ended);
                    }

                    unitOfWork.Save();

                    var idString = string.Join(", ", procedureIds);
                    this.logger.Log(LogLevel.Info, $"Changed status of Procedures {idString} to Ended");
                }
            }
            catch (OperationCanceledException ex)
            {
                this.logger.Log(LogLevel.Error, $"{this.Name} was canceled due to a token cancellation request", ex);
            }
            catch (Exception ex)
            {
                this.logger.Log(LogLevel.Error, ex.Message, ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.logger.Log(LogLevel.Debug, $"{this.Name} disposed");
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
