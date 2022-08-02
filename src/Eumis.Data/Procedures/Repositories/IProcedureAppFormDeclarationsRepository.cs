using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.DataObjects;
using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureAppFormDeclarationsRepository : IAggregateRepository<ProcedureAppFormDeclaration>
    {
        IList<ProcedureDeclarationVO> GetDeclarations(int procedureId);

        ProcedureDeclarationDO GetDeclaration(int declarationId);

        void ActivateProcedureDeclarations(int procedureId);

        bool IsDeclarationReadonly(int programmeDeclarationId);

        IList<ProcedureDeclarationJson> GetDeclarationsForProcedureVersion(int procedureId);

        Guid GetProcedureDeclarationGid(int programmeDeclarationId, int procedureId);

        List<string> ValidateProcedureDeclarations(int procedureId);
    }
}
