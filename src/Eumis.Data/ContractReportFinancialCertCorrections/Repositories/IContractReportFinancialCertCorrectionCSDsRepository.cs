using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialCertCorrections.Repositories
{
    public interface IContractReportFinancialCertCorrectionCSDsRepository : IAggregateRepository<ContractReportFinancialCertCorrectionCSD>
    {
        IList<ContractReportFinancialCertCorrectionCSD> FindAll(int contractReportFinancialCertCorrectionId);

        IList<ContractReportFinancialCertCorrectionCSD> FindAll(int contractReportFinancialCertCorrectionId, int[] contractReportFinancialCertCorrectionCSDIds);

        IList<ContractReportFinancialCertCorrectionCSD> FindAllUnattached(int contractReportFinancialCertCorrectionId);

        IList<ContractReportFinancialCertCorrectionCSD> FindAllByCertReport(int certReportId, int contractReportFinancialCertCorrectionId);

        IList<ContractReportFinancialCertCorrectionCSDsVO> GetContractReportFinancialCertCorrectionCSDs(
            int contractReportFinancialCertCorrectionId,
            string csd = null,
            string company = null,
            bool? isAttachedToCertReport = null,
            int? certReportId = null);

        bool HasContractReportFinancialCertCorrectionCSDs(int contractReportFinancialCertCorrectionId);

        bool HasDraftContractReportFinancialCertCorrectionCSDs(int contractReportFinancialCertCorrectionId);

        bool HasCertContractReportFinancialCertCorrectionCSDs(int certReportId);
    }
}
