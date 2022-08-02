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

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/shares")]
    public class ProcedureSharesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureSharesController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureSharesVO> GetProcedureShares(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureShares(procedureId);
        }

        [Route("{procedureShareId:int}")]
        public ProcedureShareDO GetProcedureShare(int procedureId, int procedureShareId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureShare = procedure.FindProcedureShare(procedureShareId);

            return new ProcedureShareDO(procedureShare, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureShareDO NewProcedureShare(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var primaryProcedureShare = procedure.FindPrimaryProcedureShare();

            return new ProcedureShareDO(
                primaryProcedureShare.ProgrammeId,
                (int?)null,
                procedureId,
                false,
                procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ProcedureShares.Create), IdParam = "procedureId")]
        public void AddProcedureShare(int procedureId, ProcedureShareDO procedureShare)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureShare.Version);

            procedure.AddProcedureShare(
                procedureShare.ProgrammeId.Value,
                procedureShare.ProgrammePriorityId.Value,
                procedureShare.BudgetAmount.BgAmount.Value,
                procedureShare.IsPrimary.Value);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureShareId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ProcedureShares.Edit), IdParam = "procedureId", ChildIdParam = "procedureShareId")]
        public void UpdateProcedureShare(int procedureId, int procedureShareId, ProcedureShareDO procedureShare)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureShare.Version);

            procedure.UpdateProcedureShare(procedureShareId, procedureShare.BudgetAmount.BgAmount.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procedureShareId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ProcedureShares.Delete), IdParam = "procedureId", ChildIdParam = "procedureShareId")]
        public void DeleteProcedureShare(int procedureId, int procedureShareId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureShare(procedureShareId);

            this.unitOfWork.Save();
        }
    }
}
