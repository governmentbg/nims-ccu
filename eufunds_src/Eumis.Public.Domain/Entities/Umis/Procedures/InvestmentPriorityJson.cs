using System;
using System.Collections.Generic;
namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class InvestmentPriorityJson
    {
        public InvestmentPriorityJson()
        {
            this.SpecificTargets = new List<SpecificTargetJson>();
        }

        public int InvestmentPriorityId { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public IList<SpecificTargetJson> SpecificTargets { get; set; }
    }
}
