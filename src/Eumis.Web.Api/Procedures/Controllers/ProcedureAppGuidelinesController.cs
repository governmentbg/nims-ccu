using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/appGuidelines")]
    public class ProcedureAppGuidelinesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureAppGuidelinesController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureAppGuidelinesVO> GetProcedureAppGuidelines(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureAppGuidelines(procedureId);
        }

        [Route("{appGuidelineId:int}")]
        public ProcedureAppGuidelineDO GetProcedureAppGuideline(int procedureId, int appGuidelineId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureAppGuideline = procedure.FindProcedureApplicationGuideline(appGuidelineId);

            return new ProcedureAppGuidelineDO(procedureAppGuideline, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureAppGuidelineDO NewProcedureAppGuideline(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureAppGuidelineDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppGuidelines.Create), IdParam = "procedureId")]
        public void AddProcedureAppGuideline(int procedureId, ProcedureAppGuidelineDO appGuideline)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, appGuideline.Version);

            procedure.AddProcedureApplicationGuideline(
                appGuideline.Name,
                appGuideline.Description,
                appGuideline.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{appGuidelineId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppGuidelines.Edit), IdParam = "procedureId", ChildIdParam = "appGuidelineId")]
        public void UpdateProcedureAppGuideline(int procedureId, int appGuidelineId, ProcedureAppGuidelineDO appGuideline)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, appGuideline.Version);

            procedure.UpdateProcedureApplicationGuideline(
                appGuidelineId,
                appGuideline.Name,
                appGuideline.Description,
                appGuideline.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{appGuidelineId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppGuidelines.Delete), IdParam = "procedureId", ChildIdParam = "appGuidelineId")]
        public void DeleteProcedureAppGuideline(int procedureId, int appGuidelineId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureApplicationGuideline(appGuidelineId);

            this.unitOfWork.Save();
        }
    }
}
