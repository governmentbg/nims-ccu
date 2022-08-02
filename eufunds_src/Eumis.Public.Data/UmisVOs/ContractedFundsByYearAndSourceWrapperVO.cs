using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class ContractedFundsByYearAndSourceWrapperVO
    {
        public IList<ContractedFundsByYearAndSourceVO> ContractedFundsByYearAndSource { get; set; }

        public IList<FinanceSource> Sources { get; set; }
    }
}
