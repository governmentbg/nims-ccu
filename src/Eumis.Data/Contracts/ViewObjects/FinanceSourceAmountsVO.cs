using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class FinanceSourceAmountsVO
    {
        public decimal? AmountBGN { get; set; }

        public decimal? AmountEUR { get; set; }
    }
}
