using System;
using System.Web.Http;
using Eumis.ApplicationServices.Services.EvalSessionSheetXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;

namespace Eumis.PortalIntegration.Api.Documents.EvalSessionStandpoints.Controllers
{
    [RoutePrefix("api/standpoints")]
    public class EvalSessionStandpointsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository;
        private IEvalSessionStandpointXmlService evalSessionStandpointXmlService;

        public EvalSessionStandpointsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository,
            IEvalSessionStandpointXmlService evalSessionStandpointXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionStandpointXmlsRepository = evalSessionStandpointXmlsRepository;
            this.evalSessionStandpointXmlService = evalSessionStandpointXmlService;
        }

        [Route("{xmlGid:guid}")]
        public XmlDO GetEvalSessionStandpointXml(Guid xmlGid)
        {
            var evalSessionId = this.evalSessionStandpointXmlsRepository.GetEvalSessionId(xmlGid);
            var evalSessionStandpointId = this.evalSessionStandpointXmlsRepository.GetEvalSessionStandpointId(xmlGid);
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(MyEvalSessionStandpointActions.View, evalSessionStandpointId),
                Tuple.Create<Enum, int?>(EvalSessionActions.ViewSessionData, evalSessionId));

            var standpoint = this.evalSessionStandpointXmlsRepository.FindByGid(xmlGid);

            return new XmlDO()
            {
                Gid = standpoint.Gid,
                Xml = standpoint.Xml,
                Version = standpoint.Version,
                ModifyDate = standpoint.ModifyDate,
            };
        }

        [HttpPut]
        [Route("{xmlGid:guid}")]
        [Transaction]
        [PessimisticLock]
        public XmlDO UpdateEvalSessionStandpointXml(Guid xmlGid, XmlDO standpointDO)
        {
            var evalSessionStandpointId = this.evalSessionStandpointXmlsRepository.GetEvalSessionStandpointId(xmlGid);
            this.authorizer.AssertCanDo(MyEvalSessionStandpointActions.Edit, evalSessionStandpointId);

            if (!this.evalSessionStandpointXmlService.CanUpdateStandpoint(xmlGid))
            {
                throw new DomainValidationException("Cannot update standpoint xml.");
            }

            var standpoint = this.evalSessionStandpointXmlsRepository.FindForUpdateByGid(xmlGid, standpointDO.Version);

            standpoint.SetXml(standpointDO.Xml);
            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = standpoint.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Standpoints.UpdateXml),
                standpoint.EvalSessionId,
                standpoint.EvalSessionStandpointId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Route("{xmlGid:guid}/submit")]
        [Transaction]
        [PessimisticLock]
        public void SubmitEvalSessionStandpointXml(Guid xmlGid, XmlDO standpointDO)
        {
            var evalSessionStandpointId = this.evalSessionStandpointXmlsRepository.GetEvalSessionStandpointId(xmlGid);
            this.authorizer.AssertCanDo(MyEvalSessionStandpointActions.Edit, evalSessionStandpointId);

            var evalSessionStandpointXml = this.evalSessionStandpointXmlsRepository.FindForUpdateByGid(xmlGid, standpointDO.Version);
            var evalSession = this.evalSessionsRepository.Find(evalSessionStandpointXml.EvalSessionId);

            evalSession.ChangeEvalSessionStandpointStatusToEnded(evalSessionStandpointXml.EvalSessionStandpointId);
            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Standpoints.ChangeStatusToEnded),
                evalSessionStandpointXml.EvalSessionId,
                evalSessionStandpointXml.EvalSessionStandpointId,
                standpointDO,
                null);
        }
    }
}
