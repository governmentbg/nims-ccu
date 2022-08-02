using System;
using System.Collections.Generic;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureEvalTableXmlsRepository : IAggregateRepository<ProcedureEvalTableXml>
    {
        ProcedureEvalTableXml FindByProcedureEvalTableId(int procedureEvalTableId);

        ProcedureEvalTableXml FindByGid(Guid procedureEvalTableXmlGid);

        ProcedureEvalTableXml FindForUpdateByGid(Guid procedureEvalTableXmlGid, byte[] version);

        IList<ProcedureEvalTableXml> FindByProcedureId(int procedureId);

        void RemoveByEvalTableId(int evalTableId);

        int GetProcedureId(Guid gid);
    }
}
