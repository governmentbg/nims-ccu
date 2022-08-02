using System;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckPlanBasicDataVO
    {
        public int SpotCheckPlanId { get; set; }

        public Year Year { get; set; }

        public Month Month { get; set; }

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
