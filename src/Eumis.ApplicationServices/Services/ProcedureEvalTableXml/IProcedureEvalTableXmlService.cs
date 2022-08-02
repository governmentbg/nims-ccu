using System;
using System.Collections.Generic;
using Eumis.Domain.Procedures;

namespace Eumis.ApplicationServices.Services.ProcedureEvalTableXml
{
    public interface IProcedureEvalTableXmlService
    {
        bool CanUpdateEvalTable(Guid evalTableGid);

        Domain.Procedures.ProcedureEvalTableXml CreateEvalTable(ProcedureEvalTable evalTable);

        void CopyProcedureEvalTableXmls(IList<Tuple<ProcedureEvalTable, string>> evalTablesWithXml);
    }
}
