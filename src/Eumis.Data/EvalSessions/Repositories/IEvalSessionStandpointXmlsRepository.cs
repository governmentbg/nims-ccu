using System;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionStandpointXmlsRepository : IAggregateRepository<EvalSessionStandpointXml>
    {
        int GetEvalSessionId(Guid gid);

        int GetEvalSessionStandpointId(Guid gid);

        EvalSessionStandpointXml FindByEvalSessionStandpointId(int standpointId);

        EvalSessionStandpointXml FindByGid(Guid gid);

        EvalSessionStandpointXml FindForUpdateByGid(Guid gid, byte[] version);
    }
}
