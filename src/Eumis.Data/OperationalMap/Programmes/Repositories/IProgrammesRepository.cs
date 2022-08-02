using System;
using System.Collections.Generic;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    public interface IProgrammesRepository : IAggregateRepository<Programme>
    {
        IList<ProgrammeBudgetsWrapperVO> GetProgrammeBudgets(int programmeId);

        IList<ProgrammeTreeVO> GetProgrammesTree();

        IList<ProgrammeProcedureManualsVO> GetProgrammeProcedureManuals(int programmeId);

        IList<ProgrammeApplicationDocumentsVO> GetProgrammeApplicationDocuments(int programmeId);

        IList<Guid> GetApplicationDocumentRelatedProcedures(int programmeApplicationDocumentId);

        IList<ProgrammeApplicationDocumentProcedureVO> GetApplicationDocumentRelatedProceduresData(int programmeApplicationDocumentId);

        bool IsProgrammeApplicationDocumentAttachedToProcedure(int programmeApplicationDocumentId);

        Dictionary<int, string> GetProgrammesIdAndShortName();

        Dictionary<int, Tuple<string, string>> GetProgrammesIdCodeAndShortName();

        int[] GetProgammeIds();

        IList<string> CanModifyProgramme(
            int? programmeId,
            string code,
            string name,
            string shortName);

        IList<string> CanEnterProgramme(int programmeId);

        IList<string> CanDeleteProgramme(int programmeId);

        string GetProgrammeCode(int programmeId);

        string GetProgrammeApplicationDocumentExtension(int programmeApplicationDocumentId);

        IList<MapNodeDirectionVO> GetProgrammeDirections(int mapNodeId);
    }
}
