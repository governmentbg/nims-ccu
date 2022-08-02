using Eumis.ApplicationServices.Services.EvalSessionSheetXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/sheets")]
    public class EvalSessionSheetsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository;
        private IEvalSessionSheetXmlService evalSessionSheetXmlService;
        private IProceduresRepository proceduresRepository;
        private IProjectsRepository projectsRepository;

        public EvalSessionSheetsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository,
            IEvalSessionSheetXmlService evalSessionSheetXmlService,
            IProceduresRepository proceduresRepository,
            IProjectsRepository projectsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionSheetXmlsRepository = evalSessionSheetXmlsRepository;
            this.evalSessionSheetXmlService = evalSessionSheetXmlService;
            this.proceduresRepository = proceduresRepository;
            this.projectsRepository = projectsRepository;
        }

        [Route("")]
        public IList<EvalSessionSheetsVO> GetEvalSessionSheets(
            int evalSessionId,
            int? project = null,
            ProcedureEvalTableType? evalTableType = null,
            int? distribution = null,
            int? assessor = null,
            [FromUri] EvalSessionSheetStatus[] statuses = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionSheets(evalSessionId, project, evalTableType, distribution, assessor, null, statuses);
        }

        [Route("{sheetId:int}")]
        public EvalSessionSheetDO GetEvalSessionSheet(int evalSessionId, int sheetId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionSheet = evalSession.FindEvalSessionSheet(sheetId);

            var evalSessionSheetXml = this.evalSessionSheetXmlsRepository.FindByEvalSessionSheetId(sheetId);

            return new EvalSessionSheetDO(evalSessionSheet, evalSession.Version, evalSessionSheetXml);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionSheetDO NewEvalSessionSheet(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new EvalSessionSheetDO(evalSessionId, evalSession.Version)
            {
                DistributionType = EvalSessionDistributionType.Manual,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Sheets.Create), IdParam = "evalSessionId")]
        public void AddEvalSessionSheet(int evalSessionId, EvalSessionSheetDO evalSessionSheet)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionSheet.Version);

            var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(evalSessionSheet.ProjectId.Value);

            var sessionSheet = evalSession.AddEvalSessionSheet(
                evalSessionSheet.EvalSessionUserId.Value,
                evalSessionSheet.ProjectId.Value,
                evalSessionSheet.EvalTableType.Value,
                evalSessionSheet.DistributionType.Value,
                evalSessionSheet.Notes,
                projectStatus);

            this.unitOfWork.Save();

            var evalSessionSheetXml = this.evalSessionSheetXmlService.CreateSheet(sessionSheet);
            this.evalSessionSheetXmlsRepository.Add(evalSessionSheetXml);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{sheetId:int}/continueSheet")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Sheets.Continue), IdParam = "evalSessionId", ChildIdParam = "sheetId")]
        public object ContinueEvalSessionSheet(int evalSessionId, int sheetId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var evalSessionSheet = evalSession.FindEvalSessionSheet(sheetId);

            if (evalSessionSheet.Status != EvalSessionSheetStatus.Paused)
            {
                throw new InvalidOperationException("Cannot continue sheet with status different from paused.");
            }

            if (evalSession.EvalSessionEvaluations.Where(t => t.IsDeleted == false && t.EvalSessionEvaluationSheets.Where(p => p.EvalSessionSheetId == sheetId).Any()).Any())
            {
                throw new InvalidOperationException("Cannot continue an EvalSessionSheet which is associated with a non-deleted EvalSessionEvaluation");
            }

            var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(evalSessionSheet.ProjectId);

            var continuedSheet = evalSession.AddEvalSessionSheet(
                evalSessionSheet.EvalSessionUserId,
                evalSessionSheet.ProjectId,
                evalSessionSheet.EvalTableType,
                EvalSessionDistributionType.Continued,
                null,
                projectStatus);

            this.unitOfWork.Save();

            continuedSheet.ContinueEvalSheet();

            evalSession.AddContinuedEvalSessionSheetId(evalSessionSheet, continuedSheet.EvalSessionSheetId);

            var evalSessionSheetXml = this.evalSessionSheetXmlsRepository.FindByEvalSessionSheetId(sheetId);
            EvalSessionSheetXml continuedSheetXml = new EvalSessionSheetXml(
                evalSessionId,
                continuedSheet.EvalSessionSheetId,
                evalSessionSheetXml);

            this.evalSessionSheetXmlsRepository.Add(continuedSheetXml);

            this.unitOfWork.Save();

            return new { EvalSessionId = evalSessionId, EvalSessionSheetId = continuedSheet.EvalSessionSheetId };
        }

        [HttpPost]
        [Route("{sheetId:int}/cancelSheet")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Sheets.ChangeStatusToCanceled), IdParam = "evalSessionId", ChildIdParam = "sheetId")]
        public void CancelEvalSessionSheet(int evalSessionId, int sheetId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.CancelEvalSessionSheet(sheetId, confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{sheetId:int}/canCancel")]
        public ErrorsDO CanCancelEvalSessionSheet(int evalSessionId, int sheetId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionsRepository.CanCancelEvalSessionSheet(evalSessionId, sheetId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("canCreate")]
        public string CanCreateEvalSessionSheet(int evalSessionId, EvalSessionSheetDO evalSessionSheet)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(evalSessionSheet.ProjectId.Value);

            var error = evalSession.CanCreateEvalSessionSheet(
                    evalSessionSheet.EvalSessionUserId.Value,
                    evalSessionSheet.ProjectId.Value,
                    evalSessionSheet.EvalTableType.Value,
                    evalSessionSheet.DistributionType.Value,
                    projectStatus);

            return error;
        }

        [HttpPost]
        [Route("{sheetId:int}/canContinue")]
        public ErrorsDO CanContinueEvalSessionSheet(int evalSessionId, int sheetId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionsRepository.CanContinueEvalSessionSheet(evalSessionId, sheetId);

            return new ErrorsDO(errorList);
        }
    }
}
