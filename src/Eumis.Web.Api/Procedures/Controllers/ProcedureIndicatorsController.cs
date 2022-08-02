using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Indicators;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/indicators")]
    public class ProcedureIndicatorsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IIndicatorsRepository indicatorsRepository;
        private IAuthorizer authorizer;

        public ProcedureIndicatorsController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IIndicatorsRepository indicatorsRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.indicatorsRepository = indicatorsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureIndicatorsVO> GetProcedureIndicators(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureIndicators(procedureId);
        }

        [Route("{indicatorId:int}")]
        public ProcedureIndicatorDO GetProcedureIndicator(int procedureId, int indicatorId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureIndicator = procedure.FindProcedureIndicator(indicatorId);

            var indicator = this.indicatorsRepository.Find(procedureIndicator.IndicatorId);

            return new ProcedureIndicatorDO(indicator, procedureIndicator, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureIndicatorDO NewProcedureIndicator(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureIndicatorDO(procedure, procedure.Version);
        }

        [HttpGet]
        [Route("newAttached")]
        public ProcedureIndicatorDO NewAttachedProcedureIndicator(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureIndicatorDO(procedureId, procedure.Version);
        }

        [HttpGet]
        [Route("hasIndicatorsForAttach")]
        public bool HasIndicatorsForAttach(int procedureId)
        {
            return this.proceduresRepository.HasAvailableIndicatorsForAttach(procedureId);
        }

        [HttpPost]
        [Route("create")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Indicators.Create), IdParam = "procedureId")]
        public void CreateAndAttachProcedureIndicator(int procedureId, ProcedureIndicatorDO procedureIndicator)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureIndicator.Version);

            Indicator indicator = new Indicator(
                procedureIndicator.Indicator.ProgrammeId.Value,
                procedureIndicator.Indicator.MeasureId.Value,
                procedureIndicator.Indicator.IndicatorItemTypeId.Value,
                procedureIndicator.Indicator.Name,
                procedureIndicator.Indicator.HasGenderDivision.Value);

            this.indicatorsRepository.Add(indicator);

            this.unitOfWork.Save();

            procedure.AddProcedureIndicator(
                indicator.IndicatorId,
                procedureIndicator.BaseTotalValue,
                procedureIndicator.BaseMenValue,
                procedureIndicator.BaseWomenValue,
                procedureIndicator.BaseYear,
                procedureIndicator.TargetTotalValue,
                procedureIndicator.TargetMenValue,
                procedureIndicator.TargetWomenValue,
                procedureIndicator.MilestoneTargetTotalValue,
                procedureIndicator.MilestoneTargetMenValue,
                procedureIndicator.MilestoneTargetWomenValue,
                procedureIndicator.DataSource,
                procedureIndicator.Description,
                procedureIndicator.Indicator.SourceMapNodeId.Value);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{indicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Indicators.Edit), IdParam = "procedureId", ChildIdParam = "indicatorId")]
        public void UpdateProcedureIndicator(int procedureId, int indicatorId, ProcedureIndicatorDO procedureIndicator)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureIndicator.Version);

            procedure.UpdateProcedureIndicator(
                indicatorId,
                procedureIndicator.BaseTotalValue,
                procedureIndicator.BaseMenValue,
                procedureIndicator.BaseWomenValue,
                procedureIndicator.BaseYear,
                procedureIndicator.TargetTotalValue,
                procedureIndicator.TargetMenValue,
                procedureIndicator.TargetWomenValue,
                procedureIndicator.MilestoneTargetTotalValue,
                procedureIndicator.MilestoneTargetMenValue,
                procedureIndicator.MilestoneTargetWomenValue,
                procedureIndicator.DataSource,
                procedureIndicator.Description);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Indicators.Attach), IdParam = "procedureId", ChildIdParam = "procedureIndicator.Indicator.IndicatorId")]
        public void AttachProcedureIndicator(int procedureId, ProcedureIndicatorDO procedureIndicator)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureIndicator.Version);

            procedure.AddProcedureIndicator(
                procedureIndicator.Indicator.IndicatorId.Value,
                procedureIndicator.BaseTotalValue,
                procedureIndicator.BaseMenValue,
                procedureIndicator.BaseWomenValue,
                procedureIndicator.BaseYear,
                procedureIndicator.TargetTotalValue,
                procedureIndicator.TargetMenValue,
                procedureIndicator.TargetWomenValue,
                procedureIndicator.MilestoneTargetTotalValue,
                procedureIndicator.MilestoneTargetMenValue,
                procedureIndicator.MilestoneTargetWomenValue,
                procedureIndicator.DataSource,
                procedureIndicator.Description,
                procedureIndicator.Indicator.SourceMapNodeId.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{indicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Indicators.Delete), IdParam = "procedureId", ChildIdParam = "indicatorId")]
        public void DetachProcedureIndicator(int procedureId, int indicatorId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureIndicator(indicatorId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{indicatorId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Indicators.Deactivate), IdParam = "procedureId", ChildIdParam = "indicatorId")]
        public void DeactivateProcedureIndicator(int procedureId, int indicatorId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureIndicator(indicatorId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{indicatorId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Indicators.Activate), IdParam = "procedureId", ChildIdParam = "indicatorId")]
        public void ActivateProcedureIndicator(int procedureId, int indicatorId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureIndicator(indicatorId);

            this.unitOfWork.Save();
        }
    }
}
