using Eumis.Data.Debts.ViewObjects;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Debts.Repositories
{
    public interface IContractDebtsRepository : IAggregateRepository<ContractDebt>
    {
        IList<ContractDebtVO> GetContractDebts(int[] programmeIds, int userId);

        IList<ContractDebtReportVO> GetContractDebtReport(int[] programmeIds, DateTime dateFrom, DateTime dateTo);

        int GetContractId(int contractDebtId);

        IList<ContractDebtInterestVO> GetContractDebtInterests(int contractDebtId);

        int GetNextContractDebtInterestOrderNum(int contractDebtId);

        IList<ContractDebtVO> GetFinancialCorrectionContractDebts(int financialCorrectionId);

        IList<ContractDebtVO> GetIrregularityContractDebts(int irregularityId);

        ContractDebtInfoVO GetInfo(int contractDebtId);

        ContractDebtStatus GetStatus(int contractDebtId);

        IList<Tuple<int, int, int>> GetContractDebtsData(int[] contractIds);

        IList<ContractDebtVO> GetContractDebtsForProjectDossier(int contractId);

        bool HasCertContractReportContractDebts(int certReportId);
    }
}
