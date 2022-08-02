using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories
{
    public interface IContractReportCertAuthorityFinancialCorrectionCSDsRepository : IAggregateRepository<ContractReportCertAuthorityFinancialCorrectionCSD>
    {
        IList<ContractReportCertAuthorityFinancialCorrectionCSD> FindAll(int contractReportCertAuthorityFinancialCorrectionId);

        IList<ContractReportCertAuthorityFinancialCorrectionCSD> FindAll(int contractReportCertAuthorityFinancialCorrectionId, int[] contractReportCertAuthorityFinancialCorrectionCSDIds);

        IList<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetContractReportCertAuthorityFinancialCorrectionCSDs(
            int contractReportCertAuthorityFinancialCorrectionId,
            string csd = null,
            string company = null);

        bool HasContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId);

        bool HasDraftContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId);
    }
}
