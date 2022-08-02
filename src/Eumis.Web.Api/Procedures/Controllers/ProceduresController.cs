using Eumis.ApplicationServices.Services.ProcedureEvalTableXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.OperationalMap.Directions.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures")]
    public class ProceduresController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IIndicatorsRepository indicatorsRepository;
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository;
        private IProcedureEvalTableXmlService procedureEvalTableXmlService;
        private IAuthorizer authorizer;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;

        public ProceduresController(
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            IProgrammePrioritiesRepository programmesRepository,
            IIndicatorsRepository indicatorsRepository,
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository,
            IProcedureEvalTableXmlService procedureEvalTableXmlService,
            IAuthorizer authorizer,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.programmePrioritiesRepository = programmesRepository;
            this.indicatorsRepository = indicatorsRepository;
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.procedureAppFormDeclarationsRepository = procedureAppFormDeclarationsRepository;
            this.procedureEvalTableXmlService = procedureEvalTableXmlService;
            this.authorizer = authorizer;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
        }

        [Route("tree")]
        public IList<ProcedureProgrammeTreeVO> GetProcedureProgrammesTree()
        {
            return this.proceduresRepository.GetProcedureProgrammesTree();
        }

        [Route("")]
        public IList<ProcedureVO> GetProcedures(int? programmeId = null, int? programmePriorityId = null)
        {
            this.authorizer.AssertCanDo(ProcedureListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProcedurePermissions.CanRead);

            return this.proceduresRepository.GetProcedures(programmeIds, programmeId, programmePriorityId);
        }

        [HttpGet]
        [Route("new")]
        public NewProcedureDO NewProcedure(int programmeId, int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.CreateProcedure, programmeId);

            var nextCode = this.GetNextProcedureCode(programmePriorityId, 2021);
            string procedureCode = nextCode.Item1;

            return new NewProcedureDO(programmeId, programmePriorityId, true, procedureCode);
        }

        [HttpPost]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Create))]
        public ProcedureDO CreateProcedure(NewProcedureDO procedure)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.CreateProcedure, procedure.ProcedureShare.ProgrammeId.Value);

            // TODO: we should check if the ProgrammePriority's programmeId match the supplied ProgrammeId
            // or ignore the programmeId altogether
            var nextCode = this.GetNextProcedureCode(procedure.ProcedureShare.ProgrammePriorityId.Value, procedure.Procedure.Year.Value);
            string procedureCode = nextCode.Item1;
            int procedureNumber = nextCode.Item3;
            DateTime procedureEndDate;

            if (procedure.Procedure.ProcedureKind == ProcedureKind.Budget)
            {
                // Set fake end date
                procedureEndDate = DateTime.Now.AddYears(1);
            }
            else
            {
                procedureEndDate = procedure.ProcedureTimeLimit.EndDate.Value.AddMilliseconds(procedure.ProcedureTimeLimit.EndTime.Value);
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Procedure newProcedure = new Procedure(
                    procedureNumber,
                    procedure.Procedure.ProcedureStatus.Value,
                    procedure.Procedure.ApplicationFormType.Value,
                    procedure.Procedure.ProcedureKind,
                    procedure.Procedure.Year.Value,
                    procedureCode,
                    procedure.Procedure.Name,
                    procedure.Procedure.NameAlt,
                    procedure.Procedure.Description,
                    procedure.Procedure.DescriptionAlt,
                    procedure.Procedure.AllowedRegistrationType,
                    procedure.Procedure.ProjectMinAmount,
                    procedure.Procedure.ProjectMinAmountInfo,
                    procedure.Procedure.ProjectMinAmountInfoAlt,
                    procedure.Procedure.ProjectMaxAmount,
                    procedure.Procedure.ProjectMaxAmountInfo,
                    procedure.Procedure.ProjectMaxAmountInfoAlt,
                    procedure.Procedure.ProjectDuration,
                    procedure.ProcedureShare.ProgrammeId.Value,
                    procedure.ProcedureShare.ProgrammePriorityId.Value,
                    procedure.ProcedureShare.BudgetAmount.BgAmount.Value,
                    procedure.ProcedureShare.IsPrimary.Value,
                    procedureEndDate,
                    procedure.ProcedureTimeLimit.Notes);

                this.proceduresRepository.Add(newProcedure);

                this.unitOfWork.Save();

                transaction.Commit();

                return new ProcedureDO(newProcedure);
            }
        }

        [HttpGet]
        [Route("{procedureId:int}/copy")]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Copy), IdParam = "procedureId")]
        public ProcedureDO CopyProcedure(int procedureId)
        {
            var oldProcedure = this.proceduresRepository.Find(procedureId);
            var procedureShare = oldProcedure.ProcedureShares.Single();

            this.authorizer.AssertCanDo(ProgrammeActions.CreateProcedure, procedureShare.ProgrammeId);

            var nextCode = this.GetNextProcedureCode(procedureShare.ProgrammePriorityId, oldProcedure.Year);
            string procedureCode = nextCode.Item1;
            int procedureNumber = nextCode.Item3;

            var procedureProgrammes = oldProcedure.ProcedureProgrammes;
            var timeLimits = oldProcedure.ProcedureTimeLimits;
            var specFields = oldProcedure.ProcedureSpecFields;
            var evalTables = oldProcedure.ProcedureEvalTables;
            var locations = oldProcedure.ProcedureLocations;

            var evalTableXmls = this.procedureEvalTableXmlsRepository.FindByProcedureId(oldProcedure.ProcedureId);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Procedure newProcedure = new Procedure(
                    procedureNumber,
                    oldProcedure.ProcedureKind,
                    ProcedureStatus.Draft,
                    oldProcedure.ApplicationFormType,
                    oldProcedure.Year,
                    procedureCode,
                    oldProcedure.Name,
                    oldProcedure.NameAlt,
                    oldProcedure.Description,
                    oldProcedure.DescriptionAlt,
                    oldProcedure.AllowedRegistrationType,
                    oldProcedure.ProjectMinAmount,
                    oldProcedure.ProjectMinAmountInfo,
                    oldProcedure.ProjectMinAmountInfoAlt,
                    oldProcedure.ProjectMaxAmount,
                    oldProcedure.ProjectMaxAmountInfo,
                    oldProcedure.ProjectMaxAmountInfoAlt,
                    oldProcedure.ProjectDuration,
                    procedureShare.ProgrammeId,
                    procedureShare.ProgrammePriorityId,
                    procedureShare.BgAmount,
                    procedureShare.IsPrimary);

                newProcedure.CopyProcedureProgrammes(procedureProgrammes);
                newProcedure.CopyProcedureTimeLimits(timeLimits);
                newProcedure.CopyProcedureSpecFields(specFields);
                newProcedure.CopyProcedureLocations(locations);
                newProcedure.CopyApplicationSections(oldProcedure.ProcedureApplicationSections);
                newProcedure.CopyProcedureDirections(oldProcedure.ProcedureDirections);
                newProcedure.CopyProcedureContractReportDocuments(oldProcedure.ProcedureContractReportDocuments);

                var evalTablesWithXml = newProcedure.CopyProcedureEvalTables(evalTables, evalTableXmls);

                this.proceduresRepository.Add(newProcedure);

                this.unitOfWork.Save();

                this.procedureEvalTableXmlService.CopyProcedureEvalTableXmls(evalTablesWithXml);

                this.unitOfWork.Save();

                transaction.Commit();

                return new ProcedureDO(newProcedure);
            }
        }

        [HttpGet]
        [Route("{procedureId:int}/gid")]
        public Guid GetProcedureGid(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetGid(procedureId);
        }

        [Route("{procedureId:int}")]
        public ProcedureDO GetProcedure(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureDO(procedure);
        }

        [HttpPut]
        [Route("{procedureId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.BasicData), IdParam = "procedureId")]
        public void UpdateProcedure(int procedureId, ProcedureDO procedure)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure oldProcedure = this.proceduresRepository.FindForUpdate(procedureId, procedure.Version);

            oldProcedure.SetAttributes(
                procedure.ProcedureKind,
                procedure.Code,
                procedure.Name,
                procedure.NameAlt,
                procedure.Description,
                procedure.DescriptionAlt,
                procedure.AllowedRegistrationType,
                procedure.ProjectMinAmount,
                procedure.ProjectMinAmountInfo,
                procedure.ProjectMinAmountInfoAlt,
                procedure.ProjectMaxAmount,
                procedure.ProjectMaxAmountInfo,
                procedure.ProjectMaxAmountInfoAlt,
                procedure.ProjectDuration);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToDraft), IdParam = "procedureId")]
        public void ChangeStatusToDraft(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetDraft, procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Draft);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToEntered")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToEntered), IdParam = "procedureId")]
        public void ChangeStatusToEntered(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetEntered, procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Entered);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToChecked), IdParam = "procedureId")]
        public void ChangeStatusToChecked(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetChecked, procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Checked);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToActive), IdParam = "procedureId")]
        public void ChangeStatusToActive(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetActive, procedureId);

            this.ActivateRelatedDocuments(procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Active);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToEnded), IdParam = "procedureId")]
        public void ChangeStatusToEnded(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetEnded, procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Ended);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToTerminated")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToTerminated), IdParam = "procedureId")]
        public void ChangeStatusToTerminated(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetTerminated, procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Terminated);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeStatusToCanceled")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeStatusToCanceled), IdParam = "procedureId")]
        public void ChangeStatusToCanceled(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetCanceled, procedureId);

            this.ChangeStatus(procedureId, Convert.FromBase64String(version), ProcedureStatus.Canceled);
        }

        [Route("{procedureId:int}/info")]
        public ProcedureInfoVO GetProcedureInfo(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var info = this.proceduresRepository.GetProcedureInfo(procedureId);

            info.IsIndicatorVisible = info.IsIndicatorVisible && !Procedure.HideIndicators;

            return info;
        }

        [HttpPost]
        [Route("{procedureId:int}/canChangeStatusToEntered")]
        public ErrorsDO CanChangeStatusToEntered(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.SetEntered, procedureId);

            return this.ValidateProcedureInternal(procedureId);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeContractReportDocumentsSectionStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeContractReportDocumentsSectionStatusToDraft), IdParam = "procedureId")]
        public void ChangeContractReportDocumentsSectionStatusToDraft(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            this.ChangeContractReportDocumentsSectionStatus(procedureId, Convert.FromBase64String(version), ProcedureContractReportDocumentsSectionStatus.Draft);
        }

        [HttpPut]
        [Route("{procedureId:int}/changeContractReportDocumentsSectionStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.ChangeContractReportDocumentsSectionStatusToActive), IdParam = "procedureId")]
        public void ChangeContractReportDocumentsSectionStatusToActive(int procedureId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            this.ChangeContractReportDocumentsSectionStatus(procedureId, Convert.FromBase64String(version), ProcedureContractReportDocumentsSectionStatus.Active);
        }

        [HttpPost]
        [Route("{procedureId:int}/validate")]
        public ErrorsDO ValidateProcedure(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.ValidateApplicationForm(procedureId);
        }

        private void ChangeStatus(int procedureId, byte[] version, ProcedureStatus procedureStatus)
        {
            Procedure oldProcedure = this.proceduresRepository.FindForUpdate(procedureId, version);

            oldProcedure.ChangeStatus(procedureStatus);

            if (!oldProcedure.ActivationDate.HasValue && procedureStatus == ProcedureStatus.Active)
            {
                oldProcedure.ActivationDate = DateTime.Now;
            }

            this.unitOfWork.Save();
        }

        private void ChangeContractReportDocumentsSectionStatus(int procedureId, byte[] version, ProcedureContractReportDocumentsSectionStatus sectionStatus)
        {
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, version);

            procedure.ChangeContractReportDocumentsSectionStatus(sectionStatus);

            this.unitOfWork.Save();
        }

        private ErrorsDO ValidateProcedureInternal(int procedureId)
        {
            Procedure oldProcedure = this.proceduresRepository.Find(procedureId);

            int[] indicatorIds = oldProcedure.GetProcedureIndicatorIds();
            var procedureIndicators = this.indicatorsRepository.FindAll(indicatorIds);

            List<string> errors = oldProcedure.Validate(procedureIndicators, !Procedure.HideIndicators).ToList();

            List<string> declarationErrors = this.procedureAppFormDeclarationsRepository.ValidateProcedureDeclarations(procedureId);
            errors.AddRange(declarationErrors);

            return new ErrorsDO(errors);
        }

        private ErrorsDO ValidateApplicationForm(int procedureId)
        {
            IList<string> errors = new List<string>();
            Procedure oldProcedure = this.proceduresRepository.Find(procedureId);

            int[] indicatorIds = oldProcedure.GetProcedureIndicatorIds();
            var procedureIndicators = this.indicatorsRepository.FindAll(indicatorIds);

            errors = oldProcedure.ValidateApplicationForm(procedureIndicators);

            return new ErrorsDO(errors);
        }

        private Tuple<string, int, int> GetNextProcedureCode(int programmePriorityId, int year)
        {
            string programmePriorityCode = this.programmePrioritiesRepository.GetProgrammePriorityCode(programmePriorityId);
            int procedureNumber = this.proceduresRepository.GetLastProcedureNumber(programmePriorityId, year) + 1;
            string procedureCode = string.Format("{0}-{1}-{2:00}", programmePriorityCode, year, procedureNumber);

            return Tuple.Create(procedureCode, year, procedureNumber);
        }

        private void ActivateRelatedDocuments(int procedureId)
        {
            this.procedureAppFormDeclarationsRepository.ActivateProcedureDeclarations(procedureId);
        }
    }
}
