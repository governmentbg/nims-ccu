using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialCorrections.Repositories
{
    public interface IContractReportFinancialCorrectionCSDsRepository : IAggregateRepository<ContractReportFinancialCorrectionCSD>
    {
        IList<ContractReportFinancialCorrectionCSD> FindAll(int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionCSD> FindAll(int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds);

        IList<ContractReportFinancialCorrectionCSD> FindAllUnattached(int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionCSD> FindAllByCertReport(int certReportId, int contractReportFinancialCorrectionId);

        IList<ContractReportFinancialCorrectionCSD> FindAllByCertReport(int certReportId);

        IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportFinancialCorrectionCSDs(
            int contractReportFinancialCorrectionId,
            string csd = null,
            string company = null,
            bool? isAttachedToCertReport = null,
            int? certReportId = null);

        bool HasContractReportFinancialCorrectionCSDs(int contractReportFinancialCorrectionId);

        bool HasDraftContractReportFinancialCorrectionCSDs(int contractReportFinancialCorrectionId);

        bool HasCertDraftContractReportFinancialCorrectionCSDs(int certReportId);

        bool HasCertContractReportFinancialCorrectionCSDs(int certReportId);
    }
}
