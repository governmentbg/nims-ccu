using Eumis.Public.Data.UmisVOs;
using System.Collections.Generic;

namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class ContractProcurementsVO : ContractCommonVO
    {
        public IEnumerable<OfferVO> Offers { get; set; }
    }
}
