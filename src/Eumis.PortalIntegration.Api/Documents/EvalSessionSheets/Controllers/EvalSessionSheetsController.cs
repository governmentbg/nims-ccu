using Eumis.ApplicationServices.Services.EvalSessionSheetXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain;
using Eumis.Domain.EvalSessions;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Documents.EvalSessionSheets.DataObjects;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.EvalSessionSheets.Controllers
{
    [RoutePrefix("api/evalSessionSheets")]
    public class EvalSessionSheetsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository;
        private IEvalSessionSheetXmlService evalSessionSheetXmlService;

        public EvalSessionSheetsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository,
            IEvalSessionSheetXmlService evalSessionSheetXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionSheetXmlsRepository = evalSessionSheetXmlsRepository;
            this.evalSessionSheetXmlService = evalSessionSheetXmlService;
        }

        [Route("{xmlGid:guid}")]
        public EvalSessionSheetXmlDO GetEvalSessionSheetXml(Guid xmlGid)
        {
            var evalSessionId = this.evalSessionSheetXmlsRepository.GetEvalSessionId(xmlGid);
            var evalSessionSheetId = this.evalSessionSheetXmlsRepository.GetEvalSessionSheetId(xmlGid);
            var projectId = this.evalSessionSheetXmlsRepository.GetProjectId(xmlGid);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(MyEvalSessionSheetActions.Edit, evalSessionSheetId),
                Tuple.Create<Enum, int?>(EvalSessionActions.ViewSessionData, evalSessionId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectId));

            var evalSessionSheetXmlData = this.evalSessionSheetXmlsRepository.GetDataByGid(xmlGid);

            return new EvalSessionSheetXmlDO(evalSessionSheetXmlData);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{xmlGid:guid}")]
        public EvalSessionSheetXmlDO UpdateEvalSessionSheetXml(Guid xmlGid, EvalSessionSheetXmlDO evalSessionSheetXmlDO)
        {
            var evalSessionSheetId = this.evalSessionSheetXmlsRepository.GetEvalSessionSheetId(xmlGid);
            this.authorizer.AssertCanDo(MyEvalSessionSheetActions.Edit, evalSessionSheetId);

            if (!this.evalSessionSheetXmlService.CanUpdateSheet(xmlGid))
            {
                throw new DomainValidationException("Cannot update sheet xml.");
            }

            var evalSessionSheetXml = this.evalSessionSheetXmlsRepository.FindForUpdateByGid(xmlGid, evalSessionSheetXmlDO.Version);

            evalSessionSheetXml.SetXml(evalSessionSheetXmlDO.Xml);
            this.unitOfWork.Save();

            var evalSessionSheetData = this.evalSessionsRepository.GetSheetData(evalSessionSheetXml.EvalSessionSheetId);

            var response = new EvalSessionSheetXmlDO(evalSessionSheetXml, evalSessionSheetData);

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Sheets.UpdateXml),
                evalSessionSheetXml.EvalSessionId,
                evalSessionSheetXml.EvalSessionSheetId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{xmlGid:guid}/submit")]
        public EvalSessionSheetXmlDO SubmitEvalSessionSheetXml(Guid xmlGid, EvalSessionSheetXmlDO evalSessionSheetXmlDO)
        {
            var evalSessionSheetId = this.evalSessionSheetXmlsRepository.GetEvalSessionSheetId(xmlGid);
            this.authorizer.AssertCanDo(MyEvalSessionSheetActions.Edit, evalSessionSheetId);

            var evalSessionSheetXml = this.evalSessionSheetXmlsRepository.FindForUpdateByGid(xmlGid, evalSessionSheetXmlDO.Version);
            var evalSession = this.evalSessionsRepository.Find(evalSessionSheetXml.EvalSessionId);

            evalSessionSheetXml.Submit();

            evalSession.ChangeEvalSessionSheetStatus(
                evalSessionSheetXml.EvalSessionSheetId,
                EvalSessionSheetStatus.Ended,
                null);
            this.unitOfWork.Save();

            var evalSessionSheetData = this.evalSessionsRepository.GetSheetData(evalSessionSheetXml.EvalSessionSheetId);

            var response = new EvalSessionSheetXmlDO(evalSessionSheetXml, evalSessionSheetData);

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Sheets.ChangeStatusToEnded),
                evalSessionSheetXml.EvalSessionId,
                evalSessionSheetXml.EvalSessionSheetId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{xmlGid:guid}/pause")]
        public EvalSessionSheetXmlDO PauseEvalSessionSheetXml(Guid xmlGid, EvalSessionSheetXmlDO evalSessionSheetXmlDO)
        {
            var evalSessionSheetId = this.evalSessionSheetXmlsRepository.GetEvalSessionSheetId(xmlGid);
            this.authorizer.AssertCanDo(MyEvalSessionSheetActions.Edit, evalSessionSheetId);

            var evalSessionSheetXml = this.evalSessionSheetXmlsRepository.FindForUpdateByGid(xmlGid, evalSessionSheetXmlDO.Version);
            var evalSession = this.evalSessionsRepository.Find(evalSessionSheetXml.EvalSessionId);

            evalSessionSheetXml.Pause();

            evalSession.ChangeEvalSessionSheetStatus(
                evalSessionSheetXml.EvalSessionSheetId,
                EvalSessionSheetStatus.Paused,
                null);
            this.unitOfWork.Save();

            var evalSessionSheetData = this.evalSessionsRepository.GetSheetData(evalSessionSheetXml.EvalSessionSheetId);

            var response = new EvalSessionSheetXmlDO(evalSessionSheetXml, evalSessionSheetData);

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Sheets.ChangeStatusToPaused),
                evalSessionSheetXml.EvalSessionId,
                evalSessionSheetXml.EvalSessionSheetId,
                null,
                null);

            return response;
        }
    }
}
