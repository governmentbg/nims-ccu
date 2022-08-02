using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/monitorstatEconomicActivities")]
    public class ProcedureMonitorstatEconomicActivitiesController : ApiController
    {
        private IAuthorizer authorizer;
        private IProcedureMonitorstatEconomicActivitiesRepository procedureMonitorstatEconomicActivitiesRepository;
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;

        public ProcedureMonitorstatEconomicActivitiesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProcedureMonitorstatEconomicActivitiesRepository procedureMonitorstatEconomicActivitiesRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.procedureMonitorstatEconomicActivitiesRepository = procedureMonitorstatEconomicActivitiesRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ProcedureMonitorstatEconomicActivityVO> GetProcedureMonitorstatEconomicActivities(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.procedureMonitorstatEconomicActivitiesRepository.GetProcedureMonitorstatEconomicActivities(procedureId);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureMonitorstatEconomicActivityDO NewProcedureMonitorstatEconomicActivity(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return new ProcedureMonitorstatEconomicActivityDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.EconomicActivities.Create), IdParam = "procedureId")]
        public void CreateProcedureMonitorstatEconomicActivity(int procedureId, ProcedureMonitorstatEconomicActivityDO activity)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedureMonitorstatEconomicActivity = new ProcedureMonitorstatEconomicActivity(procedureId, activity.Year, activity.Type);

            this.procedureMonitorstatEconomicActivitiesRepository.Add(procedureMonitorstatEconomicActivity);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProcedureMonitorstatEconomicActivity(int procedureId, ProcedureMonitorstatEconomicActivityDO activity)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var errorList = this.procedureMonitorstatEconomicActivitiesRepository
                .CanCreateProcedureMonitorstatEconomicActivity(procedureId, activity.Year);

            return new ErrorsDO(errorList);
        }

        [HttpDelete]
        [Route("{procedureMonitorstatEconomicActivityId}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.EconomicActivities.Delete), IdParam = "procedureId", ChildIdParam = "procedureMonitorstatEconomicActivityId")]
        public void DeleteProcedureMonitorstatEconomicActivity(int procedureId, int procedureMonitorstatEconomicActivityId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = Convert.FromBase64String(version);

            var activity = this.procedureMonitorstatEconomicActivitiesRepository.FindForUpdate(procedureMonitorstatEconomicActivityId, vers);
            activity.AssertIsDraft();

            this.procedureMonitorstatEconomicActivitiesRepository.Remove(activity);
            this.unitOfWork.Save();
        }
    }
}
