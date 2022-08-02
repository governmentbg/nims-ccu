using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using System.Collections.Generic;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    public interface IProgrammeAppFormDeclarationsRepository : IAggregateRepository<ProgrammeAppFormDeclaration>
    {
        IList<ProgrammeDeclarationVO> GetProgrammeDeclarations(int programmeId);

        IList<string> CanDeleteProgrammeDeclaration(int programmeDeclarationId);

        int GetNextProgrammeDeclarationOrderNum(int programmeId);

        IList<ProgrammeProcedureVO> GetDeclarationRelatedProceduresData(int programmeDeclarationId);

        IList<ProgrammeDeclarationItemVO> GetProgrammeDeclarationItems(int programmeDeclarationId);

        IList<string> CanAddProgrammeDeclarationItem(int programmeDeclarationId, int orderNum);

        IList<string> CanUpdateProgrammeDeclarationItem(int programmeDeclarationId, int programmeDeclarationItemId, int orderNum);

        IList<string> CanDeleteProgrammeDeclarationItem(int programmeDeclarationId);

        IList<string> CanCreateProgrammeDeclaration(int programmeId, string name, string nameAlt);

        ProgrammeDeclaration FindProgrammeDeclaration(int programmeDeclarationId);

        ProgrammeDeclaration GetProgrammeDeclaration(int programmeDeclarationId);
    }
}
