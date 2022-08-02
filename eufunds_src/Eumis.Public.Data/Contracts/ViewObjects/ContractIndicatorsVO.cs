using Eumis.Public.Data.UmisVOs;
using System.Collections.Generic;

namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class ContractIndicatorsVO : ContractCommonVO
    {
        public IEnumerable<ContractIndicatorVO> Indicators { get; set; }
    }
}
