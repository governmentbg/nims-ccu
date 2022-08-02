using Eumis.ApplicationServices.Communicators;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ProcedureEvalTableXml
{
    internal class ProcedureEvalTableXmlService : IProcedureEvalTableXmlService
    {
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IProceduresRepository proceduresRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public ProcedureEvalTableXmlService(
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IProceduresRepository proceduresRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.proceduresRepository = proceduresRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        public bool CanUpdateEvalTable(Guid evalTableGid)
        {
            var evalTableXml = this.procedureEvalTableXmlsRepository.FindByGid(evalTableGid);
            var procedure = this.proceduresRepository.Find(evalTableXml.ProcedureId);

            return procedure.ProcedureStatus == ProcedureStatus.Draft;
        }

        public void CopyProcedureEvalTableXmls(IList<Tuple<ProcedureEvalTable, string>> evalTablesWithXml)
        {
            foreach (var evalTableWithXml in evalTablesWithXml)
            {
                var evalTable = evalTableWithXml.Item1;
                var oldXml = evalTableWithXml.Item2;

                string newXml = this.documentRestApiCommunicator.CopyProcedureEvalTableXml(evalTable.EvalType, oldXml);

                this.procedureEvalTableXmlsRepository.Add(new Domain.Procedures.ProcedureEvalTableXml(evalTable.ProcedureId, evalTable.ProcedureEvalTableId, newXml));
            }
        }

        public Domain.Procedures.ProcedureEvalTableXml CreateEvalTable(ProcedureEvalTable evalTable)
        {
            string xml = this.documentRestApiCommunicator.CreateProcedureEvalTableXml(evalTable.EvalType);

            return new Domain.Procedures.ProcedureEvalTableXml(
                evalTable.ProcedureId,
                evalTable.ProcedureEvalTableId,
                xml);
        }
    }
}
