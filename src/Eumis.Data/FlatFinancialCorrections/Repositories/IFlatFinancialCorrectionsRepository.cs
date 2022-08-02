using Eumis.Data.FlatFinancialCorrections.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using System.Collections.Generic;

namespace Eumis.Data.FlatFinancialCorrections.Repositories
{
    public interface IFlatFinancialCorrectionsRepository : IAggregateRepository<FlatFinancialCorrection>
    {
        IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrections(int[] programmeIds);

        int GetNextOrderNumber();

        int GetProgrammeId(int flatFinancialCorrectionId);

        IList<FlatFinancialCorrectionVO> GetFlatFinancialCorrectionsForProjectDossier(int contractId);

        // get included items
        IList<FlatFinancialCorrectionProgrammePriorityItemVO> GetFlatFinancialCorrectionProgrammePriorityItems(int flatFinancialCorrectionId);

        IList<string> CanChangeFlatFinancialCorrectionToDraft(int flatFinancialCorrectionId);

        IList<FlatFinancialCorrectionProcedureItemVO> GetFlatFinancialCorrectionProcedureItems(int flatFinancialCorrectionId);

        IList<FlatFinancialCorrectionContractItemVO> GetFlatFinancialCorrectionContractItems(int flatFinancialCorrectionId);

        IList<FlatFinancialCorrectionContractContractItemVO> GetFlatFinancialCorrectionContractContractItems(int flatFinancialCorrectionId);

        // get not included items
        IList<FlatFinancialCorrectionProgrammePriorityItemVO> GetProgrammePrioritiesForFlatFinancialCorrection(int flatFinancialCorrectionId, int programmeId);

        IList<FlatFinancialCorrectionProcedureItemVO> GetProceduresForFlatFinancialCorrection(int flatFinancialCorrectionId, int programmeId);

        IList<FlatFinancialCorrectionContractItemVO> GetContractsForFlatFinancialCorrection(int flatFinancialCorrectionId, int programmeId);

        IList<FlatFinancialCorrectionContractContractItemVO> GetContractContractsForFlatFinancialCorrection(int flatFinancialCorrectionId, int contractId, int programmeId);
    }
}
