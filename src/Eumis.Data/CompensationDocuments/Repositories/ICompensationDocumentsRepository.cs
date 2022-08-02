using System.Collections.Generic;
using Eumis.Data.CompensationDocuments.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;

namespace Eumis.Data.CompensationDocuments.Repositories
{
    public interface ICompensationDocumentsRepository : IAggregateRepository<CompensationDocument>
    {
        IList<CompensationDocumentVO> GetCompensationDocuments(
            int[] programmeIds,
            CompensationDocumentType? type = null,
            CompensationDocumentStatus? status = null);

        CompensationDocumentInfoVO GetInfo(int compensationDocumentId);

        CompensationDocumentBasicDataVO GetBasicData(int compensationDocumentId);

        IList<CompensationDocumentDocVO> GetDocuments(int compensationDocumentId);

        int GetProgrammeId(int compensationDocumentId);
    }
}
