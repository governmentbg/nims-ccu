using System;
using Eumis.Data.EvalSessions.PortalViewObjects;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionSheetXmlsRepository : IAggregateRepository<EvalSessionSheetXml>
    {
        EvalSessionSheetXml FindByEvalSessionSheetId(int evalSessionSheetId);

        EvalSessionSheetXml FindByGid(Guid gid);

        EvalSessionSheetXml FindForUpdateByGid(Guid gid, byte[] version);

        EvalSessionSheetXmlData GetDataByGid(Guid gid);

        int GetEvalSessionSheetId(Guid gid);

        int GetEvalSessionId(Guid gid);

        int GetProjectId(Guid gid);
    }
}
