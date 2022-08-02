using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.Debts.DataObjects;

namespace Eumis.Domain.CertReports.DataObjects
{
    public class CertReportContractDebtDO
    {
        public CertReportContractDebtDO()
        {
        }

        public CertReportContractDebtDO(ContractDebt contractDebt, Contract contract, ContractDebtVersion contractDebtVersion, string createdByUser, int? certReportOrderNum)
        {
            this.ContractDebt = new ContractDebtDO(contractDebt, contract, certReportOrderNum);
            this.ContractDebtVersion = new ContractDebtVersionDO(contractDebtVersion, createdByUser);
        }

        public ContractDebtDO ContractDebt { get; set; }

        public ContractDebtVersionDO ContractDebtVersion { get; set; }
    }
}
