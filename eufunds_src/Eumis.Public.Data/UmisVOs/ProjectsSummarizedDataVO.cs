using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProjectsSummarizedDataVO
    {
        public ProjectsSummarizedDataVO(IList<ContractVO> contracts)
        {
            this.TotalContractsCount = contracts.Count;
            this.TotalBFPAmount = contracts.Sum(c => c.ContractedBFPAmount);
            this.TotalAmount = contracts.Sum(c => c.ContractedTotalAmount);
            this.TotalCompaniesCount = contracts.Select(c => c.CompanyUin).Distinct().Count();
        }

        public int TotalContractsCount { get; set; }

        public decimal TotalBFPAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalCompaniesCount { get; set; }
    }
}
