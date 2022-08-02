using Eumis.Data.Debts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;

namespace Eumis.Domain.CertReports.DataObjects
{
    public class CertReportDebtReimbursedAmountDO
    {
        public CertReportDebtReimbursedAmountDO()
        {
        }

        public CertReportDebtReimbursedAmountDO(
            DebtReimbursedAmount reimbursedAmount,
            ContractDebtInfoVO contractDebtInfo,
            DebtReimbursedAmountBasicDataVO reimbursedAmountBasicData)
        {
            this.ReimbursedAmount = new ReimbursedAmountDO(reimbursedAmount, contractDebtInfo.ProgrammePriorityId);
            this.ReimbursedAmountBasicData = reimbursedAmountBasicData;
        }

        public ReimbursedAmountDO ReimbursedAmount { get; set; }

        public DebtReimbursedAmountBasicDataVO ReimbursedAmountBasicData { get; set; }
    }
}
