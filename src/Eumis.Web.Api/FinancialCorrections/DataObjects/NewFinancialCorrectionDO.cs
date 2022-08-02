using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using System;

namespace Eumis.Web.Api.FinancialCorrections.DataObjects
{
    public class NewFinancialCorrectionDO
    {
        public NewFinancialCorrectionDO()
        {
        }

        public NewFinancialCorrectionDO(Contract contract)
        {
            this.Contract = new ContractDataDO(contract);
        }

        public DateTime? ImpositionDate { get; set; }

        public int? ContractContractId { get; set; }

        public int? ContractBudgetLevel3AmountId { get; set; }

        public ContractDataDO Contract { get; set; }
    }
}
