using System;
using System.Linq;
using Eumis.ApplicationServices.Communicators;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;

namespace Eumis.ApplicationServices.Services.EvalSessionSheetXml
{
    public class EvalSessionSheetXmlService : IEvalSessionSheetXmlService
    {
        private IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public EvalSessionSheetXmlService(
            IEvalSessionSheetXmlsRepository evalSessionSheetXmlsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IProceduresRepository proceduresRepository,
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.evalSessionSheetXmlsRepository = evalSessionSheetXmlsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        public bool CanUpdateSheet(Guid sheetGid)
        {
            var sheetXml = this.evalSessionSheetXmlsRepository.FindByGid(sheetGid);
            var evalSession = this.evalSessionsRepository.Find(sheetXml.EvalSessionId);
            var sheet = evalSession.FindEvalSessionSheet(sheetXml.EvalSessionSheetId);

            return evalSession.EvalSessionStatus == EvalSessionStatus.Active &&
                   sheet.Status == EvalSessionSheetStatus.Draft;
        }

        public Domain.EvalSessions.EvalSessionSheetXml CreateSheet(EvalSessionSheet sessionSheet)
        {
            var evalSession = this.evalSessionsRepository.Find(sessionSheet.EvalSessionId);

            Procedure procedure = this.proceduresRepository.Find(evalSession.ProcedureId);

            var procedureEvalTable = procedure.ProcedureEvalTables
                .Single(et => et.IsActivated && et.IsActive && et.Type == sessionSheet.EvalTableType);

            var evalTableXml = this.procedureEvalTableXmlsRepository.FindByProcedureEvalTableId(procedureEvalTable.ProcedureEvalTableId);

            string xml = this.documentRestApiCommunicator.CreateEvalSessionSheetXml(evalTableXml.Xml);

            return new Domain.EvalSessions.EvalSessionSheetXml(
                evalSession.EvalSessionId,
                sessionSheet.EvalSessionSheetId,
                procedureEvalTable.EvalType,
                procedureEvalTable.Type,
                xml);
        }
    }
}
