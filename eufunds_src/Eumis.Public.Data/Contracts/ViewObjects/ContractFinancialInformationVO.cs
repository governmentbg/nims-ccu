using Eumis.Public.Data.UmisVOs;
using System.Collections.Generic;

namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class ContractFinancialInformationVO : ContractCommonVO
    {
        public decimal ContractedEuAmount { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedSelfAmount { get; set; }

        public decimal PaidEuAmount { get; set; }

        public decimal PaidBgAmount { get; set; }

        public decimal ProcedureShareBgAmount { get; set; }

        public decimal ProcedureShareEuAmount { get; set; }

        public IEnumerable<FinancialCorrectionVO> FinancialCorrections { get; set; }

        public decimal ContractedBFPAmount
        {
            get
            {
                return this.ContractedEuAmount + this.ContractedBgAmount;
            }
        }

        public decimal ContractedTotalAmount
        {
            get
            {
                return this.ContractedBFPAmount + this.ContractedSelfAmount;
            }
        }

        public decimal PaidTotalAmount
        {
            get
            {
                return this.PaidEuAmount + this.PaidBgAmount;
            }
        }

        public decimal EuPercent
        {
            get
            {
                if (this.ContractedTotalAmount > 0)
                {
                    return this.ProcedureShareEuAmount / (this.ProcedureShareBgAmount + this.ProcedureShareEuAmount) * 100;
                }
                else
                {
                    return 0m;
                }
            }
        }
    }
}
