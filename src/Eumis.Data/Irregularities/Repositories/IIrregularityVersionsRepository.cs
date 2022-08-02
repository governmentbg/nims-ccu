using System.Collections.Generic;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    public interface IIrregularityVersionsRepository : IAggregateRepository<IrregularityVersion>
    {
        IList<IrregularityVersionVO> GetVersions(int irregularityId);

        IList<IrregularityInvolvedPersonVO> GetInvolvedPersons(int versionId);

        IList<IrregularityDocVO> GetDocuments(int versionId);

        IrregularityVersion GetActiveVersion(int irregularityId);

        IrregularityStatus GetIrregularityStatus(int versionId);

        void RemoveByIrregularityId(int irregularityId);

        bool HasDraftVersions(int irregularityId);

        bool HasNonDraftVersions(int irregularityId);

        int GetProgrammeId(int versionId);

        int? GetContractId(int versionId);

        int GetIrregularityId(int versionId);
    }
}
