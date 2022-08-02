using System;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckBasicDataVO
    {
        public int SpotCheckId { get; set; }

        public int? SpotCheckPlanId { get; set; }

        public SpotCheckType Type { get; set; }

        public SpotCheckStatus Status { get; set; }

        public string RegNumber { get; set; }

        public SpotCheckLevel Level { get; set; }

        public int? ContractId { get; set; }

        public string ProgrammeCode { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeRegulationNumber { get; set; }

        public DateTime? ProgrammeRegulationDate { get; set; }

        public UinType? ProgrammeCompanyUinType { get; set; }

        public string ProgrammeCompanyUin { get; set; }

        public string ProgrammeCompanyName { get; set; }

        public byte[] Version { get; set; }
    }
}
