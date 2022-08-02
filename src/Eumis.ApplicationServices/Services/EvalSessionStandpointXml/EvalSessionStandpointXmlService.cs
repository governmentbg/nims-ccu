using System;
using Eumis.ApplicationServices.Communicators;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;

namespace Eumis.ApplicationServices.Services.EvalSessionSheetXml
{
    internal class EvalSessionStandpointXmlService : IEvalSessionStandpointXmlService
    {
        private IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public EvalSessionStandpointXmlService(
            IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.evalSessionStandpointXmlsRepository = evalSessionStandpointXmlsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        public bool CanUpdateStandpoint(Guid standpointGid)
        {
            var standpointXml = this.evalSessionStandpointXmlsRepository.FindByGid(standpointGid);
            var evalSession = this.evalSessionsRepository.Find(standpointXml.EvalSessionId);
            var standpoint = evalSession.FindEvalSessionStandpoint(standpointXml.EvalSessionStandpointId);

            return evalSession.EvalSessionStatus == EvalSessionStatus.Active &&
                   standpoint.Status == EvalSessionStandpointStatus.Draft;
        }

        public Domain.EvalSessions.EvalSessionStandpointXml CreateStandpoint(EvalSessionStandpoint sessionStandpoint)
        {
            string xml = this.documentRestApiCommunicator.CreateEvalSessionStandpointXml();

            return new Domain.EvalSessions.EvalSessionStandpointXml(
                sessionStandpoint.EvalSessionId,
                sessionStandpoint.EvalSessionStandpointId,
                xml);
        }
    }
}
