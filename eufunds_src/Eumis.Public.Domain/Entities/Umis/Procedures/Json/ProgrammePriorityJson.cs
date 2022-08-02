using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class ProgrammePriorityJson
    {
        public int ProgrammePriorityId { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public IList<InvestmentPriorityJson> InvestmentPriorities { get; set; }

        public IList<FinanceSource> FinanceSources { get; set; }
    }
}
