using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammePrioritiesSummarizedDataVO
    {
        public ProgrammePrioritiesSummarizedDataVO(IList<PPFundsWithProcedureFundsVO> programmePriorities, int contractsCount, int companiesCount)
        {
            this.TotalContractsCount = contractsCount;
            this.TotalBFPAmount = programmePriorities.Sum(pp => pp.Procedures.Sum(p => p.ContractedBgAmount + p.ContractedEuAmount));
            this.TotalAmount = programmePriorities.Sum(pp => pp.Procedures.Sum(p => p.ContractedTotalAmount));
            this.TotalCompaniesCount = companiesCount;
        }

        public int TotalContractsCount { get; set; }

        public decimal TotalBFPAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalCompaniesCount { get; set; }
    }
}
