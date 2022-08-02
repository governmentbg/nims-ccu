using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories
{
    public interface IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository : IAggregateRepository<ContractReportRevalidationCertAuthorityFinancialCorrectionCSD>
    {
        IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            string csd = null,
            string company = null);

        bool HasContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        bool HasDraftContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId);
    }
}
