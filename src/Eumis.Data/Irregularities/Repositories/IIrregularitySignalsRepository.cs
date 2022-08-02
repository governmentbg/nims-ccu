using System.Collections.Generic;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    public interface IIrregularitySignalsRepository : IAggregateRepository<IrregularitySignal>
    {
        int GetProgrammeId(int irregularitySignalId);

        IList<IrregularitySignalVO> GetIrregularitySignals(
            int[] programmeIds,
            int userId,
            int? contractId = null,
            IrregularitySignalStatus? status = null);

        IrregularitySignalInfoVO GetInfo(int irregularitySignalId);

        IrregularitySignalBasicDataVO GetBasicData(int irregularitySignalId);

        IList<IrregularityDocVO> GetDocuments(int irregularitySignalId);

        IList<IrregularityInvolvedPersonVO> GetInvolvedPersons(int irregularitySignalId);

        bool HasAssociatedIrregularity(int irregularitySignalId);

        bool HasAssociatedNonRemovedIrregularity(int irregularitySignalId);

        bool HasNonRemovedIrregularityWithTheSameNumber(int programmeId, int irregularitySignalId, string signalNumber);

        bool HasRemovedIrregularityWithTheSameNumber(int programmeId, int irregularitySignalId, string signalNumber);

        IList<IrregularitySignalVO> GetIrregularitySignalsForProjectDossier(int projectId, int? contractId);

        IList<IrregularitySignalRegisterItemVO> GetIrregularitySignalRegister(
            int[] programmeIds,
            int userId,
            int? irregularitySignalId = null);

        IList<IrregularitySignalRegisterInvolvedPersonVO> GetSignalReportInvolvedPersons(int[] irregularitySignalIds);

        int? GetContractId(int irregularitySignalId);
    }
}
