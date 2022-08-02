using System;
using Eumis.Domain.Audits;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Audits.ViewObjects
{
    public class AuditBasicDataVO
    {
        public int AuditId { get; set; }

        public AuditLevel Level { get; set; }

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
