using System;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureVersionsRepository : IAggregateRepository<ProcedureVersion>
    {
        ProcedureVersion GetLastVersion(int procedureId);

        ProcedureInfoPVO GetPortalProcedureInfo(Guid procedureGid);

        ProcedureVersion GetPortalProcedureVersion(Guid procedureGid);
    }
}
